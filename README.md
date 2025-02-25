<h1 align="center">🎣 My Phishing Case - ABBank 🎣</h1>

## I.The price to pay for laziness

After the success of the Phishing campaign targeting SeaBank, I was assigned the task of launching the next Phishing campaign against ABBank. However, as described, I suffered two consecutive failures in this campaign due to my laziness.
In Phishing campaigns, reconnaissance of AV, EDR solutions, etc., is mandatory. You can use Google Dorking to search for leaked solution policies, or identify specific public agents for certain EDR and AV solutions, or simply leverage resources from previous Red Team campaigns. After that, setting up and testing locally is crucial. Unfortunately, in this campaign, I completely skipped all these fundamental steps 😃.

## II. Reconnaissance to Create a Target List
The Recon step was quite basic: searching for email lists (use search engine like IntelX) for brute-force purposes and looking for publicly accessible webmail portals to log in.

As per the usual procedure, the priority was to find internal email accounts.Implement brute-force , in this case, I managed to find a considerable number of accessible email accounts, including notification accounts and user accounts.(I still recommend using ruler-linux64 along with a password list, as it has proven to be highly effective.)OK, everything seems to be going smoothly in the beginning😂.

## III.The first failure in the campaign

Resting on the laurels of the SeaBank campaign, I skipped all the setup steps.The account I used in this campaign was a notification account, the list of target users was very large and all randomly selected and of course, the payload I reused remained completely unchanged LOL.
<p align="center">
  <img src="https://github.com/user-attachments/assets/7c9a5fa0-0e55-407b-b873-304c58230b35">
</p>
After confidently hitting send to the list of users, I was sure that this wouldn’t take more than five minutes.And then, A few hours passed with no positive signals. I reassured myself that I had launched the campaign close to the end of the workday when no one checks their emails. Perhaps by tomorrow morning, there would be connections back to my Command-Control system.

The next morning, I woke up early, hoping for a miracle. But of course—nothing happened. I still couldn’t understand why I had failed. Then, just a little while later, the client who hired us started complaining that many of their users’ machines were infected with malware and demanded that we clean up that shit.WHAT? Really? If that’s the case, then why am I still empty-handed?

They confirmed that users had indeed executed our payload. So where did I go wrong?I immediately started searching through the documentation from previous Red Team operations.FUCK - do you know what I found?
<p align="center">
  <img src="https://github.com/user-attachments/assets/44c68a73-0c4d-4854-80cf-f9866f95de5c">
</p>

So, PowerShell was no longer usable, and my malware didn’t execute at all.However, the funniest part? The bank had absolutely no idea what was happening and was completely convinced that we had everything under control 😅.

This failure was quite serious because the number of target users was limited, and I had already used up almost all of them for this campaign (Unwritten rule - you don’t phish the same target twice, remember?) and at the same time, it made their monitoring systems and security policies even stricter.

## III.The second failure in the campaign

After the first failure, my simple thought was, 'If they don’t let me run PowerShell, I’ll just switch to running an EXE' - LOL.(Of course, their Mail Gateway allowed .EXE files. We can check which file formats are permitted by simply sending a payload to our own account).

Thus, when it comes to payloads in EXE format, the first thing that comes to mind is definitely 'Shellcode Loader.' 😅

Creating a Shellcode Loader is extremely simple:  
&emsp;&emsp;1.Generate a raw payload (exitfunc=Thread) using Cobalt Strike → data.bin.  
&emsp;&emsp;2.Encrypt data.bin with the PowerShell script commented in the provided loader.cs (using XOR encryption).  
&emsp;&emsp;3.This results in a new payload → xor_data.bin.  
&emsp;&emsp;4.Compile loader.cs with xor_data.bin as a resource using the following command:```csc /out:loader.exe /resource:xor_data.bin loader.cs```  
So, what’s special about this loader? The answer is - nothing at all. It’s just the usual P/Invoke tricks, along with a decryption algorithm to decode the previously encoded payload and load it into memory using Windows API calls.However, the test results were quite successful. It ran smoothly against Windows Defender, and the VirusTotal scan results were decent(Not recommended to use VirusTotal unless you’ve completely lost faith in yourself):
<p align="center">
  <img src="https://github.com/user-attachments/assets/e13582ba-28e4-4f37-8c3f-392c8f4b89fe">
</p>

Since this was probably my first time seeing it in C#, I thought it seemed special and decided to use it for my second campaign.I still stubbornly reused the old scenario with a different notification account. The only thing I changed was the properties of the Link Shortcut file 🤷:
```
C:\Windows\System32\cmd.exe .\Thong_tin_cve\Thong_tin_chi_tiet.exe
```
And of course, after half a day, I got nothing again.

I had a hunch about why I failed.Regaining my composure, I restarted my recon from scratch, aiming to gather information about the AV solution the organization was using.Through Google Dorking and Subdomain Enumeration, I identified two security solutions in use: Symantec EDR and McAfee Antivirus:
<p align="center">
  <img src="https://github.com/user-attachments/assets/8919cea7-dca8-4fd6-9e06-8a6e7fac0e4e">
</p>  
<p align="center">McAfee public Agent</p>  

<p align="center">
  <img src="https://github.com/user-attachments/assets/c7a74827-cc78-4975-81c4-90421e7c54ad">
</p>  
<p align="center">Decision on Extending Symantec EDR</p>  

Based on the timeline, I noticed that McAfee seemed to have been implemented more recently than Symantec EDR. So, I hypothesized that McAfee had replaced Symantec EDR.I immediately signed up for a trial of the latest version of McAfee, installed it, and conducted local testing.The result? The moment I dropped my Shellcode Loader, the system instantly deleted it and raised an alert. It didn’t even last half a second before being wiped out😂.
The question here is—why was it deleted?Technically, the code itself was completely harmless before execution.After some testing, I realized that McAfee automatically eliminates any file containing P/Invoke calls to Windows APIs commonly used for shellcode injection, as well as the Marshal.Copy method from .NET (These are legitimate APIs, by the way - so now I’m wondering, is McAfee actually a type of malware? 😂)

Injecting shellcode without using Windows APIs or .NET functions is extremely difficult.So instead of trying to make the .exe format work, why not switch to a different format that is less monitored by AV?In this case, .DLL is a strong candidate. Why? Because:  
&emsp;&emsp;+)DLL files cannot run independently—they must be executed via rundll32. Files that cannot execute on their own are generally monitored less than standalone executables(as long as we modify the signature to be different).  
&emsp;&emsp;+)DLL files can be loaded regardless of their format (even as .xlsx), making them highly flexible - another way to bypass Mail Gateway.  

## III.Success 50% (from my perspective 🤷) in the third campaign.  
Now that we have a plan, it's time to execute!  
The content of the .DLL file will be in the following format:  
<p align="center">
  <img src="https://github.com/user-attachments/assets/4f0e0867-746a-4517-9ae9-5d05c1fdd181">
</p>
Basically, it is no different from the previously used C# Shellcode Loader: it uses VirtualAlloc to allocate memory for the current process, memcpy to write the shellcode into the allocated memory, and executes the shellcode by running a thread through a function pointer.

Setup local and test:
<p align="center">
  <img src="https://github.com/user-attachments/assets/671847f5-bf2c-4143-af93-3497f1a11488">
</p>  
<p align="center">
  <img src="https://github.com/user-attachments/assets/2a1fd77a-32b4-4e80-95ad-5b70a8d42bf8">
</p>  
======> Done!  

Re-weaponizing the payload:  
.LNK attributes: 

```C:\Windows\System32\rundll32.exe .\Danh_Sach_Yeu_Cau_Cap_Chung_Tu.xlsx,Main```  
<p align="center">
  <img src="https://github.com/user-attachments/assets/b32eccd9-a59b-4f6b-8b41-585737f4ffc4">
</p>   
======> Done!

Everything seems fine. The final step is selecting an account to send the malware. During the previous reconnaissance process, I had gained access to a user account from the "Accounting Documents" department. Therefore, this time, I will create a scenario targeting the Finance department.After listing the users, I found about 50 people(Use exchanger.py to dump all information from the domain and filter employees belonging to the target department through string search) working in this department, along with a sample email that had been used before. Finally, my scenario is as follows:
<p align="center">
  <img src="https://github.com/user-attachments/assets/b9498baf-b375-47b8-afee-2bffe26e8995">
</p>  
Simple, right 🤷? But quite effective.

Right after 8 AM today (the employees' working hours), I proceeded to launch the final campaign.Not long after (about 5 minutes later), I received a connection back with 9 sessions. Yay🎉🎉🎉!
<p align="center">
  <img src="https://github.com/user-attachments/assets/00926bbf-edc4-48d8-a430-a9aa4e4c416a">
</p> 
But things didn’t stop there. All sent and received data seemed to be intercepted. I tried everything to execute any action on the client, but it was useless. Using tcpdump on the Command-Control server, I noticed that traffic was still being sent and received regularly, but none of my beacons containing commands returned any results.The reason? Easy to spot. They might have deployed another EDR - likely Symantec EDR, as I found earlier. With EDR in place, if no bypass techniques are applied, my beacon would be killed instantly as soon as it reached the client.I was quite complacent, thinking that deploying two security solutions would be costly and unlikely for the target.Secondly, obtaining an installer for Symantec EDR was quite difficult, and since there was no trial version available, I had overlooked it.

## IV. Conclusion
Although the campaign was not entirely successful, I realized the immense potential of phishing. Even after failing twice in this project, the third attempt was successful. At the same time, I gained many valuable lessons from this campaign.

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

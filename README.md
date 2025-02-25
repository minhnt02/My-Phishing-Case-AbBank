<h1 align="center">🎣 My Phishing Case - ABBank 🎣</h1>

## I.The price to pay for laziness

After the success of the Phishing campaign targeting SeaBank, I was assigned the task of launching the next Phishing campaign against ABBank. However, as described, I suffered two consecutive failures in this campaign due to my laziness.
In Phishing campaigns, reconnaissance of AV, EDR solutions, etc., is mandatory. You can use Google Dorking to search for leaked solution policies, or identify specific public agents for certain EDR and AV solutions, or simply leverage resources from previous Red Team campaigns. After that, setting up and testing locally is crucial. Unfortunately, in this campaign, I completely skipped all these fundamental steps 😃.

## II. Reconnaissance to Create a Target List
The Recon step was quite basic: searching for email lists (use search engine like IntelX) for brute-force purposes and looking for publicly accessible webmail portals to log in.

As per the usual procedure, the priority was to find internal email accounts.Implement brute-force , in this case, I managed to find a considerable number of accessible email accounts, including notification accounts and user accounts.(I still recommend using ruler-linux64 along with a password list, as it has proven to be highly effective.)OK, everything seems to be going smoothly in the beginning😂.

## III.The first failure in the campaign

Resting on the laurels of the SeaBank campaign, I skipped all the setup steps, reused the old payload with the same scenario, and sent it to a large number of users with the intent of achieving a quick victory.The account I used in this campaign was a notification account, and of course, the target users were all selected randomly.

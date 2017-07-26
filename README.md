# kontur-server-test-task

This is my old test task for c-sharp developer position. Honestly, now it seems to me a bit ugly =)

The goal was to create 2 applications:
  * Server capable to return suggests like autocomplete, list of words passed in file on startup
  * Client to this server
  
I used [https://en.wikipedia.org/wiki/Trie](trie data structure). 
Trie realization was picked from [https://www.nuget.org/packages/VDS.Common/](VDS.common) and modeified to store limited amount of data in nodes 
for better perfomance and memory consumption. 

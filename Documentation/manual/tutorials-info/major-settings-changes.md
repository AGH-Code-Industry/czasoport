# Major settings changes

Some settings chages might be the key to game performance, stability and quality. Some settings may break the game entirely.  
This page will list the most important settings that have been changed, why they were changed and what the old values were. In case of any errors in the future, please refer to this page and check if some change might be the reason for it.

> [!IMPORTANT]
> Each time an important setting is changed, MSC should be created and documented here and on corresponding pull request. Template for MSC can be found at the end of source code for this page.

## [MSC #1] Api Compatability Level
|MSC #1|Api Compatability Level|
|:---:|:---:|
|**Pull Request**|[Input Lock](https://github.com/AGH-Code-Industry/czasoport/pull/17?fbclid=IwAR0m9YdmZkSeLcmpnBly8w03c_HsRFT-9wb0ER5V6YqysUXUwfuMoc2YcI0)|
|**Setting**|`Project Settings -> Player -> Other Settings -> Configuration -> Api Compatability Level`|
|**Old Value**|.NET Standard 2.1|
|**New Value**|.NET Framework|
|**Reason for change**|Ability to use `dynamic` type|
|**Possible implications**|Worse cross-platform compatibility, problems with building for different platforms might arise|

<!-- TEMPLATE

|MSC #1|Api Compatability Level|
|:---:|:---:|
|**Pull Request**|[NAME OF PR](LINK TO PR)|
|**Setting**|`PATH -> TO -> SETTING`|
|**Old Value**|OLD VALUE|
|**New Value**|NEW VALUE|
|**Reason for change**|WHY DID U CHANGE THAT|
|**Possible implications**|WHAT MIGHT HAPPEN BECAUSE OF THAT| -->
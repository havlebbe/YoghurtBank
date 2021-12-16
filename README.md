![example workflow](https://github.com/kaeppen/YoghurtBank/actions/workflows/build-and-test.yml/badge.svg) <br>
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

# YoghurtBank

to delete all bin and obj folder run the following command:
Get-ChildItem .\ -include bin,obj -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse }

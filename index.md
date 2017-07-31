## Welcome to My Portfolio

This is Kwang-ill's portfolio site. The main purpose is to maintain my portfolios online.

These portfolios are inspired / resumed / re-created from my old projects.

Hope Github Page is the last settlement of my portfolios.

Github Rocks!


### ORACLE JOB SYNTAX MAKER (2013)

It creates syntax for scheduling job in oracle.

[ORACLE JOB SYNTAX MAKER](/portfolios/syntax_maker1.0/syntax_maker.html)
![Image](/portfolios/img/SyntaxMaker_thumb01.png)


### LINQ.js (2013)

neuecc <ils@neue.cc> made a javascript library that imitate LINQ on .Net Framework.
And it is impressively fast & usable. I made a playground page for test & fun.

[LINQ.js playground](/portfolios/linq_js_test/linq_js_test.html)
![Image](/portfolios/img/HDIMS_thumb01.png)


### Big Grid / Datatable (2011)

**Followings are my experience in past 2011, they may not be true THESE DAY.**

In 2011, My clients wants to use their Microsoft Excel in their browser-based application without installing Microsoft Excel on their workstation.

We were making an application with ExtJS which is one of famous javascript-based framework in 2011. it satisfied most functionalities except only one thing. "Grid Performance"

Actually performance of ExtJS grid is fine in ordinary case. But my client wanted MSExcel-like, browser-based, application integrated grid that should perform fast like MSExcel even when it has many data to handle. And my clients never compromise with it. Therefore our misery began.

```markdown
- Performance & Functionality
 0) renders 150 columns x oever 1000 rows. smooth horizontal & vertical scrolls.
 1) editable and selectable cells. CRUD to/from Database.
 2) popup grid & linear chart on the main grid.
 3) Copy & Paste cells
 4) Each cells contains their own background-color, font-style.
```

We tried almost (almost) every JS grid in 2011. Followings are JS Grids that I still remember.
1.**ExtJS** : has all functionality that we needed. But clients complained about Loading time and scrolling
2.**dhtmlx** : much faster than ExtJS, But it doesn't have an essential functionality.
3.**DOJO** : much faster than ExtJS, has all functionality that is needed. But still un-acceptable performance.
 
All of these javascript-based grid took about 7~10 seconds to load only for rendering. And scrolling was awfully laggy. Newer ExtJs grid used lazy rendering to avoid laggy scrolling, it shows sudden empty grid while scrolling. 

After all those test, we notices following
1. Row count affects performance little. Most of grids used "Row Virtualization" for better performance.
2. Less columns = better loading time and scrolling.
3. Smaller screen = better scrolling
4. There is no javascript-based grid that uses "Column Virtualization". So it is impossible to make a fast grid application with many columns, unless the grid supports "Column Virtualization". 

Browsers uses limited resources. Even if we makes "Column Virtualization" in javascript, the performance of javascript was not promising. So we turn our eyes to RIA(Rich Internet Applications). 

There was a little choice of RIA. Flash, Flex, Silverlight, X-Platform... we choose Silverlight, just because we were developing on .NET Framework.


In 2011, Infragistics and Telerik were two famous frameworks using Silverlight.
1. Silverlight Grid : Muuuuuch faster than javascript-based grids. But poor functionality.
2. Infragistics : Much faster than javascript-based grids.
3. Telerik : Slightly faster than Infragistics. also RadGridView uses "UI Virtualization" which support column virtualization! 

Unfortunately, javascript examples are remaining in ASPX format which is not supported in Git Page.
But I made Silverlight example for test. **Waring** This link may cause freezing on low spec computer.
[147 x 1000 Example, Siverlight(Telerik)](/HDIMS_portfolio/WebApplication1/HDIMSAPPTestPage.html)
![Image](/img/HDIMS_thumb01.png)
As you test the link. it is still slow with many data. But believe me. javascript was slower. 

So my conclution is 
`If you have to make a grid that has many columns. It is better to persuade clients to use Excel or Google sheet instead.
But if you failed, use a grid that supports "Column Virtualization".`


This link is for Google sheet for test.
[147 x 1000 Example, Google sheet](https://docs.google.com/spreadsheets/d/1sQnBhdMUh2IRDMYgWctAuk3ovoTtXcMirNiB075OEiU/edit?usp=sharing)

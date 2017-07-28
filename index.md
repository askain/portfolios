## Welcome to My Portfolio

This is Kwang-ill's portfolio site. The main purpose is to maintain my portfolios online and share them with my interviewers.

These portfolios are inspired / resumed / re-created from my old projects.

Hope Github Page is last settlement of my portfolios.

Github Rocks!


### Big Grid / Datatable (2011)

** Followings are my experience in past 2011, they might not be true THESE DAY. **

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
```markdown
1.**ExtJS** : has all functionality that we needed. But clients complained about Loading time and scrolling
2.**dhtmlx** : much faster than ExtJS, But it doesn't have an essential functionality.
3.**DOJO** : much faster than ExtJS, has all functionality that is needed. But still un-acceptable performance.
```
 
All of these javascript-based grid took about 7~10 seconds to load only for rendering. And scrolling was awfully laggy. Newer ExtJs grid used lazy rendering to avoid laggy scrolling, it shows sudden empty grid while scrolling. 

After all those test, we notices following
```markdown
1. Smaller screen = better performance
2. Row count doesn't affect performance. Most of good grids use "Row Virtualization" for better performance.
3. Less columns = better performance
4. There is no javascript-based grid use "Column Virtualization". So it is impossible to make a fast grid application with javascript-based grid. 
```

So we turn our eyes to RIA(Rich Internet Applications). In RIA, we doesn't have much choice. We choose Silverlight, just because we were developing with .NET Framework.
In 2011, Infragistics and Telerik were two famous frameworks using Silverlight.

```markdown
1. Silverlight Grid : Muuuuuch faster than javascript-based grids. But poor functionality.
2. Infragistics : Much faster than javascript-based grids.
3. Telerik : Slightly faster than Infragistics. also RadGridView uses "UI Virtualization" which support column virtualization! 
```

For test, My Telerik application were still remaining here. [Go Test](/HDIMS_portfolio/WebApplication1/HDIMSAPPTestPage.html) 



Conclusion : If you have to make a grid that has many columns. Use a grid that supports "Column Virtualization".





Markdown is a lightweight and easy-to-use syntax for styling your writing. It includes conventions for

```markdown
Syntax highlighted code block

# Header 1
## Header 2
### Header 3

- Bulleted
- List

1. Numbered
2. List

**Bold** and _Italic_ and `Code` text

[Link](url) and ![Image](src)
```

For more details see [GitHub Flavored Markdown](https://guides.github.com/features/mastering-markdown/).

### Jekyll Themes

Your Pages site will use the layout and styles from the Jekyll theme you have selected in your [repository settings](https://github.com/askain/portfolios/settings). The name of this theme is saved in the Jekyll `_config.yml` configuration file.

### Support or Contact

Having trouble with Pages? Check out our [documentation](https://help.github.com/categories/github-pages-basics/) or [contact support](https://github.com/contact) and we’ll help you sort it out.

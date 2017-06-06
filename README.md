<div>
<h2>Tutorial Overview</h2>
<p><span style="font-family:verdana,sans-serif">In this tutorial, we will create a simple Xamarin.Forms application with the FreshMvvm MVVM framework.&nbsp; The solution will target Android, iOS, and Windows 10 (UWP).&nbsp; We will create a simple Contacts application that shows a list of user contacts and an associated contact edit screen.&nbsp; To save the contact data, I will demonstrate how to use SQLite.Net as the persistence framework.</span></p>
<p><span style="font-family:verdana,sans-serif">This tutorial covers the following topics:</span></p>
<ul><li><span style="font-family:verdana,sans-serif">Creating a new Xamarin Forms solution in Visual Studio</span></li>
<li><span style="font-family:verdana,sans-serif">Adding and updating the necessary NuGet packages</span></li>
<li><span style="font-family:verdana,sans-serif">Setting a proper Build Configuration for the solution</span></li>
<li><span style="font-family:verdana,sans-serif">Creating the SQLite data model and associated Repository class</span></li>
<li><span style="font-family:verdana,sans-serif">Initializing the platform-specific database connection string and using IoC to use it in the PCL</span></li>
<li><span style="font-family:verdana,sans-serif">Creating the Page and PageModel classes that will drive the application</span></li>
<li><span style="font-family:verdana,sans-serif">Using FreshMvvm's built-in navigation class to define the main page</span></li>
<li><span style="font-family:verdana,sans-serif">Running the application <br>
</span></li></ul>
<h2><font face="verdana,sans-serif"><span style="font-family:arial,sans-serif">What is FreshMvvm?</span><b> </b></font></h2>
<font face="verdana,sans-serif">FreshMvvm is an MVVM framework created from the ground up to augment Xamarin Forms, primarily by filling in the gaps that currently exist in the framework.&nbsp; Created by <a href="https://au.linkedin.com/in/michaelridland" target="_blank">Michael Ridland</a></font><font face="verdana,sans-serif">, it adds some core features to Xamarin.Forms that developers have come to rely on in an Mvvm framework.&nbsp; <br>
<br>
<b>These features include:</b><br>
</font>
<ul><li><font face="verdana,sans-serif">An IoC Container (using TinyIoC as the back-end)</font></li>
<li><font face="verdana,sans-serif">Navigation between View Models (called Page Models in FreshMvvm)</font></li>
<li><font face="verdana,sans-serif">Automatic View/ViewModel wiring (Page and PageModel to honor Forms' naming convention)</font></li>
<li><font face="verdana,sans-serif">Navigation containers to provide an easy to work with navigation framework</font></li></ul>
<p><font face="verdana,sans-serif">Going forward, we will use the naming conventions as intended in Xamarin.Forms and FreshMvvm.&nbsp; Since the primary View classes in Xamarin.Forms are called Pages, we will utilize this naming convention for our views and view models.&nbsp; SomePage and SomePageModel describe the Some view and Some view model, respectively.</font></p>
<p><font face="verdana,sans-serif">You can find out more about FreshMvvm <a href="http://www.michaelridland.com/xamarin/freshmvvm-mvvm-framework-designed-xamarin-forms/" target="_blank">here</a>.</font><br>
</p>
<h2><span style="font-family:verdana,sans-serif"><span style="font-family:arial,sans-serif">Source Code</span></span><br>
</h2>
<p><span style="font-family:verdana,sans-serif">The
 source code for this tutorial series can be found on my GitHub page.</span></p>
<p><br>
<span style="font-family:verdana,sans-serif"><b>On GitHub:</b> <a href="https://github.com/C0D3Name/FreshWithSQLite" target="_blank">https://github.com/C0D3Name/FreshWithSQLite</a></span></p>
<hr>
<h2>Create the FreshWithSQLite solution<br>
</h2>
<h3><a name="TOC-Overview1"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<div>
<p><span style="font-family:verdana,sans-serif">In
 this section,
 we will create a new solution that will contain all our common and 
platform-specific projects.&nbsp; We will create our solution using a 
built-in Xamarin Forms solution template.&nbsp; I will also briefly go over 
updating the NuGet dependencies once our solution is created, and we have gotten rid of our unneeded files/projects.</span><b><span style="font-family:verdana,sans-serif"><br>
</span></b></p>
<h3><a name="TOC-Steps"></a><span style="font-family:verdana,sans-serif">Steps</span></h3>
</div>
<p><font face="verdana,sans-serif">Open a new instance of Visual Studio and select File &gt; New Project.</font><font face="verdana,sans-serif">&nbsp; Search for 'Xamarin Forms' and select 'Blank Xaml App (Xamarin.Forms Portable)' option.<br>
Name the solution 'XamFormsWpf' and click OK.&nbsp; <br>
</font></p>
<div style="display:block;text-align:left"><font face="verdana,sans-serif"><a href="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/01_CreateSolution.PNG?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/01_CreateSolution.PNG" style="width:100%" border="0"></a></font></div>
<br>
<font face="verdana,sans-serif">At some point during the 
initialization, you will be asked to determine which version of Windows 
10 you want to target.&nbsp; Go ahead and take the defaults here.<b><br>
</b></font>
<hr>
<h2>Remove unneeded files/projects<br>
</h2>
<h3><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif">There are some default files and projects that are added by the solution template which we will not be using in this tutorial.&nbsp; This tutorial will target Android, iOS, and UWP, so we will remove the Windows 8.1 and Windows Phone 8.1 projects that were created for us.&nbsp; We also won't be using the default MainPage.xaml file that is created in our PCL, and we can remove the GettingStarted.Xamarin file to keep the project clean.</span></p>
<h3><a name="TOC-Steps"></a><span style="font-family:verdana,sans-serif">Steps</span></h3>
<ol><li><span style="font-family:verdana,sans-serif">Right-click the FreshWithSQLite.Windows (Windows 8.1) project and select Remove.</span></li>
<li><span style="font-family:verdana,sans-serif">Right-click the FreshWithSQLite.WinPhone (Windows Phone 8.1) project and select Remove.</span></li>
<li><span style="font-family:verdana,sans-serif">In the FreshWithSQLite (Portable) project, our PCL project, delete the GettingStarted.Xamarin file.</span></li>
<li><span style="font-family:verdana,sans-serif">In the PCL project, delete the MainPage.xaml file.</span></li>
<li><b><span style="font-family:verdana,sans-serif">Open the App.xaml.cs file in the PCL, delete the line MainPage = new FreshWithSQLite.MainPage().</span></b></li>
</ol>
<p><span style="font-family:verdana,sans-serif">When finished, your solution should look like this</span></p>
<div style="display:block;text-align:left">
<div style="display:block;text-align:left"><a href="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/03_SolutionStructure.PNG?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/03_SolutionStructure.PNG" border="0"></a></div>
<br>
</div>
<div>
<hr>
<h2>Updating the existing solution NuGet packages<br>
</h2>
<h3><span style="font-family:verdana,sans-serif">Overview</span></h3>
<font face="verdana,sans-serif">At
 the time of this writing, Xamarin Forms and the Android Support Library
 NuGet packages are out of sync.&nbsp; Updating Xamarin Forms downgrades the 
Android Support Libraries, and Upgrading these libraries downgrades 
Xamarin Forms.&nbsp; <br>
<br>
This results in a never-ending upgrade loop, 
with some random NuGet errors sprinkled in that you would probably 
prefer to avoid.&nbsp; My recommendation is to upgrade Xamarin.Forms, but 
leave the Android support libraries alone.</font><br>
<h3><span style="font-family:verdana,sans-serif">Steps</span></h3>
<p><font size="2"><span style="font-family:verdana,sans-serif">From the Visual Studio menu, select Tools &gt; NuGet Package Manger &gt; Manage Packages for Solution. <br>
</span></font></p>
<p><font size="2"><span style="font-family:verdana,sans-serif">Select
 the 'Updates' tab, and check the checkboxes for 'Microsoft.NETCore.UniversalWindowsPlatform' and 'Xamarin.Forms'<br>
</span></font></p>
<p><font size="2"><span style="font-family:verdana,sans-serif">Click the Update button, the update may take a while, and you may be prompted to restart VS to complete the update.<br>
</span></font></p>
<p><font size="2"><span style="font-family:verdana,sans-serif">Once NuGet is finished updating, go ahead and build the solution, you should have no errors.<br>
</span></font></p>
<p><font size="2"><span style="font-family:verdana,sans-serif">*Note - the specific packages to update may be different, as these change over time.<br>
</span></font></p>
<p><font size="2"><span style="font-family:verdana,sans-serif">Here is a screenshot of my available Updates at the time of this writing.</span></font></p>
<div style="display:block;text-align:left"><font size="2"><a href="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/02_NuGetUpdates.PNG?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/02_NuGetUpdates.PNG" style="width:100%" border="0"></a></font></div>
<font size="2"><br>
</font></div>
<hr>
<h2>Add FreshMvvm via NuGet<br>
</h2>
<h3><span style="font-family:verdana,sans-serif">Overview</span></h3>
<font face="verdana,sans-serif">Now we need to add the FreshMvvm package to our projects to gain access to the framework.&nbsp; Each project needs a reference to this package.</font><br>
<h3><span style="font-family:verdana,sans-serif">Steps</span></h3>
<font size="2"><span style="font-family:verdana,sans-serif">From the Visual Studio menu, select Tools &gt; NuGet Package Manger &gt; Manage Packages for Solution.&nbsp; Select the Browse tab and search for FreshMvvm.&nbsp; Check the check boxes for FreshWithSQLite and FreshWithSQLite.UWP, then install.<br>
</span></font>
<div style="display:block;text-align:left"><a href="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/04_NuGetFreshMvvm.PNG?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/04_NuGetFreshMvvm.PNG" style="width:100%" border="0"></a></div>
<br>
<hr>
<h2>Add sqlite-net-pcl via NuGet<br>
</h2>
<h3><span style="font-family:verdana,sans-serif">Overview</span></h3>
<font face="verdana,sans-serif">Now
 we need to add the sqlite-net-pcl package to our projects to gain access to 
the framework.&nbsp; Each project needs a reference to this package</font>.<br>
<h3><span style="font-family:verdana,sans-serif">Steps</span></h3>
<font size="2"><span style="font-family:verdana,sans-serif">From
 the Visual Studio menu, select Tools &gt; NuGet Package Manger &gt; 
Manage Packages for Solution.&nbsp; Select the Browse tab and search for 
FreshMvvm.&nbsp; Check all the check boxes and click install.<br>
<br>
</span></font></div>
<div><font size="2"><span style="font-family:verdana,sans-serif">
<div style="display:block;text-align:left"><a href="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/05_NuGetSqlite.PNG?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/05_NuGetSqlite.PNG" style="width:100%" border="0"></a></div>
</span></font><br>
<hr>
<h2>Build Configuration Manager<br>
</h2>
<h3><span style="font-family:verdana,sans-serif">Overview</span></h3>
<font face="verdana,sans-serif">Before we get started with the project implementation, now is a good time to configure our project build settings.&nbsp; This allows us to specify our default build parameters when building and running each project.</font><br>
<h3><span style="font-family:verdana,sans-serif">Steps</span></h3>
<font size="2"><span style="font-family:verdana,sans-serif">From the Visual Studio menu, select Build &gt; Configuration Manager.&nbsp; Update the window to match the following screenshot.<br>
<br>
</span></font></div>
<div><font size="2"><span style="font-family:verdana,sans-serif">Once finished, go ahead and rebuild the solution for a sanity check.<br>
</span></font></div>
<div><font size="2"><span style="font-family:verdana,sans-serif">
<div style="display:block;text-align:left"><a href="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/06_BuildConfiguration.PNG?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/06_BuildConfiguration.PNG" style="width:100%" border="0"></a></div>
</span></font><br>
<hr>
<h2>Creating a Model and Repository<br>
</h2>
<h3><span style="font-family:verdana,sans-serif">Overview</span></h3>
<font face="verdana,sans-serif">Now our solution configuration is complete, and we can get started with the fun part, writing our code!&nbsp; In this section, we will create a Contact model that will be utilized by SQLite.Net to define our database schema.&nbsp; We will also create our simple Repository class, which will handle our database operations.</font><br>
<h3><span style="font-family:verdana,sans-serif">Create the Contact model</span><br>
</h3>
<p><font size="2"><span style="font-family:verdana,sans-serif">SQLite-Net

 uses class and property attributes to define the schema in a code-first
 approach.&nbsp; We use these attributes to define the Contact table, along with
 its columns.&nbsp; We also add a simple validation method which ensures we 
have a contact name before saving.</span></font></p>
<p><font size="2"><span style="font-family:verdana,sans-serif">In the PCL project, create a new folder called 'Models'.&nbsp; Inside this folder, create a new Bill class and add the following code</span></font><br>
</p>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> SQLite;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> System;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.Models
{
    <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
    <span style="color:rgb(153,153,136);font-style:italic">/// This class uses attributes that SQLite.Net can recognize</span>
    <span style="color:rgb(153,153,136);font-style:italic">/// and use to create the table schema.</span>
    <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
    [Table(nameof(Contact))]
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> Contact
    {
        [PrimaryKey, AutoIncrement]
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">int</span>? Id { get; <span style="color:rgb(0,134,179)">set</span>; }

        [NotNull, MaxLength(<span style="color:rgb(0,128,128)">250</span>)]
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(0,134,179)">string</span> Name { get; <span style="color:rgb(0,134,179)">set</span>; }

        [MaxLength(<span style="color:rgb(0,128,128)">250</span>)]
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(0,134,179)">string</span> Email { get; <span style="color:rgb(0,134,179)">set</span>; }

        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">bool</span> IsValid()
        {
            <span style="color:rgb(51,51,51);font-weight:bold">return</span> (!String.IsNullOrWhiteSpace(Name));
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIFNRTGl0ZTs8YnI+dXNpbmcgU3lzdGVtOzxicj48YnI+bmFtZXNwYWNl IEZyZXNoV2l0aFNRTGl0ZS5Nb2RlbHM8YnI+ezxicj7CoMKgwqAgLy8vICZsdDtzdW1tYXJ5Jmd0 Ozxicj7CoMKgwqAgLy8vIFRoaXMgY2xhc3MgdXNlcyBhdHRyaWJ1dGVzIHRoYXQgU1FMaXRlLk5l dCBjYW4gcmVjb2duaXplPGJyPsKgwqDCoCAvLy8gYW5kIHVzZSB0byBjcmVhdGUgdGhlIHRhYmxl IHNjaGVtYS48YnI+wqDCoMKgIC8vLyAmbHQ7L3N1bW1hcnkmZ3Q7PGJyPsKgwqDCoCBbVGFibGUo bmFtZW9mKENvbnRhY3QpKV08YnI+wqDCoMKgIHB1YmxpYyBjbGFzcyBDb250YWN0PGJyPsKgwqDC oCB7PGJyPsKgwqDCoCDCoMKgwqAgW1ByaW1hcnlLZXksIEF1dG9JbmNyZW1lbnRdPGJyPsKgwqDC oCDCoMKgwqAgcHVibGljIGludD8gSWQgeyBnZXQ7IHNldDsgfTxicj48YnI+wqDCoMKgIMKgwqDC oCBbTm90TnVsbCwgTWF4TGVuZ3RoKDI1MCldPGJyPsKgwqDCoCDCoMKgwqAgcHVibGljIHN0cmlu ZyBOYW1lIHsgZ2V0OyBzZXQ7IH08YnI+PGJyPsKgwqDCoCDCoMKgwqAgW01heExlbmd0aCgyNTAp XTxicj7CoMKgwqAgwqDCoMKgIHB1YmxpYyBzdHJpbmcgRW1haWwgeyBnZXQ7IHNldDsgfTxicj48 YnI+wqDCoMKgIMKgwqDCoCBwdWJsaWMgYm9vbCBJc1ZhbGlkKCk8YnI+wqDCoMKgIMKgwqDCoCB7 PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIHJldHVybiAoIVN0cmluZy5Jc051bGxPcldoaXRlU3Bh Y2UoTmFtZSkpOzxicj7CoMKgwqAgwqDCoMKgIH08YnI+wqDCoMKgIH08YnI+fTxicj4KYGBg">​</div>
</div>
</div>
<div>
<h3><span style="font-family:verdana,sans-serif">Create the Repository class<br>
</span></h3>
<font size="2"><span style="font-family:verdana,sans-serif"><font size="2"><span style="font-family:verdana,sans-serif"><font size="2"><span style="font-family:verdana,sans-serif">Our Repository class will be responsible for establishing a connection to
 the database file, creating the schema, and performing our CRUD 
operations.&nbsp; In this solution, we will only be creating and reading contact records.&nbsp; The example demonstrates utilizing the built-in asynchronous 
methods available in SQLite-net.</span></font><br>
<br>
</span></font>In the root of the PCL project, create a new class called 'Repository' and add the following code<br>
</span></font>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshWithSQLite.Models;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> SQLite;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> System;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> System.Collections.Generic;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> System.Threading.Tasks;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.Core
{
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> Repository
    {
        <span style="color:rgb(51,51,51);font-weight:bold">private</span> readonly SQLiteAsyncConnection conn;

        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(0,134,179)">string</span> StatusMessage { get; <span style="color:rgb(0,134,179)">set</span>; }

        <span style="color:rgb(51,51,51);font-weight:bold">public</span> Repository(<span style="color:rgb(0,134,179)">string</span> dbPath)
        {
            conn = <span style="color:rgb(51,51,51);font-weight:bold">new</span> SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync&lt;Contact&gt;().Wait();
        }

        <span style="color:rgb(51,51,51);font-weight:bold">public</span> async Task CreateContact(Contact contact)
        {
            <span style="color:rgb(51,51,51);font-weight:bold">try</span> {
                <span style="color:rgb(153,153,136);font-style:italic">// Basic validation to ensure we have a contact name.</span>
                <span style="color:rgb(51,51,51);font-weight:bold">if</span>(<span style="color:rgb(0,134,179)">string</span>.IsNullOrWhiteSpace(contact.Name))
                    <span style="color:rgb(51,51,51);font-weight:bold">throw</span> <span style="color:rgb(51,51,51);font-weight:bold">new</span> Exception(<span style="color:rgb(221,17,68)">"Name is required"</span>);

                <span style="color:rgb(153,153,136);font-style:italic">// Insert/update contact.</span>
                var result = await conn.InsertOrReplaceAsync(contact).ConfigureAwait(continueOnCapturedContext: <span style="color:rgb(51,51,51);font-weight:bold">false</span>);
                StatusMessage = $<span style="color:rgb(221,17,68)">"{result} record(s) added [Contact Name: {contact.Name}])"</span>;
            }
            <span style="color:rgb(51,51,51);font-weight:bold">catch</span>(Exception ex) {
                StatusMessage = $<span style="color:rgb(221,17,68)">"Failed to create contact: {contact.Name}. Error: {ex.Message}"</span>;
            }
        }

        <span style="color:rgb(51,51,51);font-weight:bold">public</span> Task&lt;List&lt;Contact&gt;&gt; GetAllContacts()
        {
            <span style="color:rgb(153,153,136);font-style:italic">// Return a list of bills saved to the Bill table in the database.</span>
            <span style="color:rgb(51,51,51);font-weight:bold">return</span> conn.Table&lt;Contact&gt;().ToListAsync();
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIEZyZXNoV2l0aFNRTGl0ZS5Nb2RlbHM7PGJyPnVzaW5nIFNRTGl0ZTs8 YnI+dXNpbmcgU3lzdGVtOzxicj51c2luZyBTeXN0ZW0uQ29sbGVjdGlvbnMuR2VuZXJpYzs8YnI+ dXNpbmcgU3lzdGVtLlRocmVhZGluZy5UYXNrczs8YnI+PGJyPm5hbWVzcGFjZSBGcmVzaFdpdGhT UUxpdGUuQ29yZTxicj57PGJyPsKgwqDCoCBwdWJsaWMgY2xhc3MgUmVwb3NpdG9yeTxicj7CoMKg wqAgezxicj7CoMKgwqAgwqDCoMKgIHByaXZhdGUgcmVhZG9ubHkgU1FMaXRlQXN5bmNDb25uZWN0 aW9uIGNvbm47PGJyPjxicj7CoMKgwqAgwqDCoMKgIHB1YmxpYyBzdHJpbmcgU3RhdHVzTWVzc2Fn ZSB7IGdldDsgc2V0OyB9PGJyPjxicj7CoMKgwqAgwqDCoMKgIHB1YmxpYyBSZXBvc2l0b3J5KHN0 cmluZyBkYlBhdGgpPGJyPsKgwqDCoCDCoMKgwqAgezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBj b25uID0gbmV3IFNRTGl0ZUFzeW5jQ29ubmVjdGlvbihkYlBhdGgpOzxicj7CoMKgwqAgwqDCoMKg IMKgwqDCoCBjb25uLkNyZWF0ZVRhYmxlQXN5bmMmbHQ7Q29udGFjdCZndDsoKS5XYWl0KCk7PGJy PsKgwqDCoCDCoMKgwqAgfTxicj48YnI+wqDCoMKgIMKgwqDCoCBwdWJsaWMgYXN5bmMgVGFzayBD cmVhdGVDb250YWN0KENvbnRhY3QgY29udGFjdCk8YnI+wqDCoMKgIMKgwqDCoCB7PGJyPsKgwqDC oCDCoMKgwqAgwqDCoMKgIHRyeSB7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIMKgwqDCoCAvLyBC YXNpYyB2YWxpZGF0aW9uIHRvIGVuc3VyZSB3ZSBoYXZlIGEgY29udGFjdCBuYW1lLjxicj7CoMKg wqAgwqDCoMKgIMKgwqDCoCDCoMKgwqAgaWYoc3RyaW5nLklzTnVsbE9yV2hpdGVTcGFjZShjb250 YWN0Lk5hbWUpKTxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDCoMKgwqAgwqDCoMKgIHRocm93IG5l dyBFeGNlcHRpb24oIk5hbWUgaXMgcmVxdWlyZWQiKTs8YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAg wqDCoMKgIDxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDCoMKgwqAgLy8gSW5zZXJ0L3VwZGF0ZSBj b250YWN0Ljxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDCoMKgwqAgdmFyIHJlc3VsdCA9IGF3YWl0 IGNvbm4uSW5zZXJ0T3JSZXBsYWNlQXN5bmMoY29udGFjdCkuQ29uZmlndXJlQXdhaXQoY29udGlu dWVPbkNhcHR1cmVkQ29udGV4dDogZmFsc2UpOzxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDCoMKg wqAgU3RhdHVzTWVzc2FnZSA9ICQie3Jlc3VsdH0gcmVjb3JkKHMpIGFkZGVkIFtDb250YWN0IE5h bWU6IHtjb250YWN0Lk5hbWV9XSkiOzxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCB9PGJyPsKgwqDC oCDCoMKgwqAgwqDCoMKgIGNhdGNoKEV4Y2VwdGlvbiBleCkgezxicj7CoMKgwqAgwqDCoMKgIMKg wqDCoCDCoMKgwqAgU3RhdHVzTWVzc2FnZSA9ICQiRmFpbGVkIHRvIGNyZWF0ZSBjb250YWN0OiB7 Y29udGFjdC5OYW1lfS4gRXJyb3I6IHtleC5NZXNzYWdlfSI7PGJyPsKgwqDCoCDCoMKgwqAgwqDC oMKgIH08YnI+wqDCoMKgIMKgwqDCoCB9PGJyPjxicj7CoMKgwqAgwqDCoMKgIHB1YmxpYyBUYXNr Jmx0O0xpc3QmbHQ7Q29udGFjdCZndDsmZ3Q7IEdldEFsbENvbnRhY3RzKCk8YnI+wqDCoMKgIMKg wqDCoCB7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIC8vIFJldHVybiBhIGxpc3Qgb2YgYmlsbHMg c2F2ZWQgdG8gdGhlIEJpbGwgdGFibGUgaW4gdGhlIGRhdGFiYXNlLjxicj7CoMKgwqAgwqDCoMKg IMKgwqDCoCByZXR1cm4gY29ubi5UYWJsZSZsdDtDb250YWN0Jmd0OygpLlRvTGlzdEFzeW5jKCk7 PGJyPsKgwqDCoCDCoMKgwqAgfTxicj7CoMKgwqAgfTxicj59PGJyPgpgYGA=">​</div>
</div>
<hr>
<h2>Add a FileAccessHelper class to each UI project<br>
</h2>
<h3><a name="TOC-Overview"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif">Our
PCL is capable of creating and accessing the SQLite database; 
however, each platform stores this database file in a different 
location.&nbsp; As a result, each project will need to provide our Repository
 class with the correct path to the file.&nbsp; To facilitate this, we will 
create a 'FileAccessHelper' class in each project, and implement 
platform specific logic to obtain the file path for the database.</span></p>
<p><span style="font-family:verdana,sans-serif">We
 will then update the Setup class' CreateApp() method in each of our UI 
projects to initialize and register our Repository as a singleton, and 
making it available to our PCL classes.</span><br>
</p>
<h3><a name="TOC-Create-a-FileAccessHelper-in-each-UI-project"></a><span style="font-family:verdana,sans-serif">Steps</span></h3>
<span style="font-family:verdana,sans-serif">In FreshWithSQLite.Droid, create a new class called FileAccessHelper with the following code<br>
</span>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.Droid
{
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> FileAccessHelper
    {
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">static</span> <span style="color:rgb(0,134,179)">string</span> GetLocalFilePath(<span style="color:rgb(0,134,179)">string</span> filename)
        {
            <span style="color:rgb(153,153,136);font-style:italic">// Use the SpecialFolder enum to get the Personal folder on the Android file system.</span>
            <span style="color:rgb(153,153,136);font-style:italic">// Storing the database here is a best practice.</span>
            <span style="color:rgb(0,134,179)">string</span> path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            <span style="color:rgb(51,51,51);font-weight:bold">return</span> System.IO.Path.Combine(path, filename);
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPm5hbWVzcGFjZSBGcmVzaFdpdGhTUUxpdGUuRHJvaWQ8YnI+ezxicj7CoMKgwqAg cHVibGljIGNsYXNzIEZpbGVBY2Nlc3NIZWxwZXI8YnI+wqDCoMKgIHs8YnI+wqDCoMKgIMKgwqDC oCBwdWJsaWMgc3RhdGljIHN0cmluZyBHZXRMb2NhbEZpbGVQYXRoKHN0cmluZyBmaWxlbmFtZSk8 YnI+wqDCoMKgIMKgwqDCoCB7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIC8vIFVzZSB0aGUgU3Bl Y2lhbEZvbGRlciBlbnVtIHRvIGdldCB0aGUgUGVyc29uYWwgZm9sZGVyIG9uIHRoZSBBbmRyb2lk IGZpbGUgc3lzdGVtLjxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCAvLyBTdG9yaW5nIHRoZSBkYXRh YmFzZSBoZXJlIGlzIGEgYmVzdCBwcmFjdGljZS48YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgc3Ry aW5nIHBhdGggPSBTeXN0ZW0uRW52aXJvbm1lbnQuR2V0Rm9sZGVyUGF0aChTeXN0ZW0uRW52aXJv bm1lbnQuU3BlY2lhbEZvbGRlci5QZXJzb25hbCk7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIHJl dHVybiBTeXN0ZW0uSU8uUGF0aC5Db21iaW5lKHBhdGgsIGZpbGVuYW1lKTs8YnI+wqDCoMKgIMKg wqDCoCB9PGJyPsKgwqDCoCB9PGJyPn08YnI+CmBgYA==">​</div>
</div>
<font face="verdana,sans-serif">In FreshWithSQLite.iOS, create a new class called FileAccessHelper with the following code<br>
</font>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> System;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.iOS
{
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> FileAccessHelper
    {
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">static</span> <span style="color:rgb(0,134,179)">string</span> GetLocalFilePath(<span style="color:rgb(0,134,179)">string</span> filename)
        {
            <span style="color:rgb(153,153,136);font-style:italic">// Use the SpecialFolder enum to get the Personal folder on the iOS file system.</span>
            <span style="color:rgb(153,153,136);font-style:italic">// Then get or create the Library folder within this personal folder.</span>
            <span style="color:rgb(153,153,136);font-style:italic">// Storing the database here is a best practice.</span>
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = System.IO.Path.Combine(docFolder, <span style="color:rgb(221,17,68)">".."</span>, <span style="color:rgb(221,17,68)">"Library"</span>);

            <span style="color:rgb(51,51,51);font-weight:bold">if</span>(!System.IO.Directory.Exists(libFolder)) {
                System.IO.Directory.CreateDirectory(libFolder);
            }

            <span style="color:rgb(51,51,51);font-weight:bold">return</span> System.IO.Path.Combine(libFolder, filename);
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIFN5c3RlbTs8YnI+PGJyPm5hbWVzcGFjZSBGcmVzaFdpdGhTUUxpdGUu aU9TPGJyPns8YnI+wqDCoMKgIHB1YmxpYyBjbGFzcyBGaWxlQWNjZXNzSGVscGVyPGJyPsKgwqDC oCB7PGJyPsKgwqDCoCDCoMKgwqAgcHVibGljIHN0YXRpYyBzdHJpbmcgR2V0TG9jYWxGaWxlUGF0 aChzdHJpbmcgZmlsZW5hbWUpPGJyPsKgwqDCoCDCoMKgwqAgezxicj7CoMKgwqAgwqDCoMKgIMKg wqDCoCAvLyBVc2UgdGhlIFNwZWNpYWxGb2xkZXIgZW51bSB0byBnZXQgdGhlIFBlcnNvbmFsIGZv bGRlciBvbiB0aGUgaU9TIGZpbGUgc3lzdGVtLjxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCAvLyBU aGVuIGdldCBvciBjcmVhdGUgdGhlIExpYnJhcnkgZm9sZGVyIHdpdGhpbiB0aGlzIHBlcnNvbmFs IGZvbGRlci48YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgLy8gU3RvcmluZyB0aGUgZGF0YWJhc2Ug aGVyZSBpcyBhIGJlc3QgcHJhY3RpY2UuPGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIHZhciBkb2NG b2xkZXIgPSBFbnZpcm9ubWVudC5HZXRGb2xkZXJQYXRoKEVudmlyb25tZW50LlNwZWNpYWxGb2xk ZXIuUGVyc29uYWwpOzxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCB2YXIgbGliRm9sZGVyID0gU3lz dGVtLklPLlBhdGguQ29tYmluZShkb2NGb2xkZXIsICIuLiIsICJMaWJyYXJ5Iik7PGJyPjxicj7C oMKgwqAgwqDCoMKgIMKgwqDCoCBpZighU3lzdGVtLklPLkRpcmVjdG9yeS5FeGlzdHMobGliRm9s ZGVyKSkgezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDCoMKgwqAgU3lzdGVtLklPLkRpcmVjdG9y eS5DcmVhdGVEaXJlY3RvcnkobGliRm9sZGVyKTs8YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgfTxi cj48YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgcmV0dXJuIFN5c3RlbS5JTy5QYXRoLkNvbWJpbmUo bGliRm9sZGVyLCBmaWxlbmFtZSk7PGJyPsKgwqDCoCDCoMKgwqAgfTxicj7CoMKgwqAgfTxicj59 PGJyPgpgYGA=">​</div>
</div>
<font face="verdana,sans-serif">In FreshWithSQLite.UWP, create a new class called FileAccessHelper with the following code<br>
</font>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.UWP
{
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> FileAccessHelper
    {
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">static</span> <span style="color:rgb(0,134,179)">string</span> GetLocalFilePath(<span style="color:rgb(0,134,179)">string</span> filename)
        {
            <span style="color:rgb(153,153,136);font-style:italic">// For UWP, we store the database file in our application data's local folder.</span>
            var path = global::Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            <span style="color:rgb(51,51,51);font-weight:bold">return</span> System.IO.Path.Combine(path, filename);
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPm5hbWVzcGFjZSBGcmVzaFdpdGhTUUxpdGUuVVdQPGJyPns8YnI+wqDCoMKgIHB1 YmxpYyBjbGFzcyBGaWxlQWNjZXNzSGVscGVyPGJyPsKgwqDCoCB7PGJyPsKgwqDCoCDCoMKgwqAg cHVibGljIHN0YXRpYyBzdHJpbmcgR2V0TG9jYWxGaWxlUGF0aChzdHJpbmcgZmlsZW5hbWUpPGJy PsKgwqDCoCDCoMKgwqAgezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCAvLyBGb3IgVVdQLCB3ZSBz dG9yZSB0aGUgZGF0YWJhc2UgZmlsZSBpbiBvdXIgYXBwbGljYXRpb24gZGF0YSdzIGxvY2FsIGZv bGRlci48YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgdmFyIHBhdGggPSBnbG9iYWw6OldpbmRvd3Mu U3RvcmFnZS5BcHBsaWNhdGlvbkRhdGEuQ3VycmVudC5Mb2NhbEZvbGRlci5QYXRoOzxicj7CoMKg wqAgwqDCoMKgIMKgwqDCoCByZXR1cm4gU3lzdGVtLklPLlBhdGguQ29tYmluZShwYXRoLCBmaWxl bmFtZSk7PGJyPsKgwqDCoCDCoMKgwqAgfTxicj7CoMKgwqAgfTxicj59PGJyPgpgYGA=">​</div>
</div>
<br>
<hr>
<h2>Initialize the Repository and register it with FreshIOC<br>
</h2>
<h3><a name="TOC-Overview"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif">Since each platform needs to provide a custom database path to the repository, we will need to initialize the repository in each project.&nbsp; To make this instance available in our PCL, we will then use FreshMvvm's built-in IoC container to register the instance.&nbsp; FreshMvvm's IoC is strongly based on TinyIoC, so if you have any experience working with TinyIoC, the mechanisms should be familiar.&nbsp; This will allow us to work with the Repository class in the PCL, and ensure it is properly initialized for each platform.<br>
</span></p>
<h3><a name="TOC-Create-a-FileAccessHelper-in-each-UI-project"></a><span style="font-family:verdana,sans-serif">Steps</span></h3>
<span style="font-family:verdana,sans-serif"><b>FreshWithSQLite.Droid</b><br>
<br>
Open the MainActivity.cs class and add add the following lines to the OnCreate method, just before LoadApplication(new App()) is called:</span><br>
</div>
<div><span style="font-family:verdana,sans-serif">
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%">var repository = <span style="color:rgb(51,51,51);font-weight:bold">new</span> Repository(FileAccessHelper.GetLocalFilePath(<span style="color:rgb(221,17,68)">"contacts.db3"</span>));
FreshIOC.Container.Register(repository);
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnZhciByZXBvc2l0b3J5ID0gbmV3IFJlcG9zaXRvcnkoRmlsZUFjY2Vzc0hlbHBl ci5HZXRMb2NhbEZpbGVQYXRoKCJjb250YWN0cy5kYjMiKSk7PGJyPkZyZXNoSU9DLkNvbnRhaW5l ci5SZWdpc3RlcihyZXBvc2l0b3J5KTs8YnI+CmBgYA==">​</div>
</div>
The full MainActivity class should now look similar to this:</span><br>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.App;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.Content.PM;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.OS;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshMvvm;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshWithSQLite.Core;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.Droid
{
     [Activity(Label = <span style="color:rgb(221,17,68)">"FreshWithSQLite"</span>, Icon = <span style="color:rgb(221,17,68)">"@drawable/icon"</span>, Theme =  <span style="color:rgb(221,17,68)">"@style/MainTheme"</span>, MainLauncher = <span style="color:rgb(51,51,51);font-weight:bold">true</span>, ConfigurationChanges =  ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        <span style="color:rgb(51,51,51);font-weight:bold">protected</span> override <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(<span style="color:rgb(51,51,51);font-weight:bold">this</span>, bundle);

            var repository = <span style="color:rgb(51,51,51);font-weight:bold">new</span> Repository(FileAccessHelper.GetLocalFilePath(<span style="color:rgb(221,17,68)">"contacts.db3"</span>));
            FreshIOC.Container.Register(repository);

            LoadApplication(<span style="color:rgb(51,51,51);font-weight:bold">new</span> App());
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIEFuZHJvaWQuQXBwOzxicj51c2luZyBBbmRyb2lkLkNvbnRlbnQuUE07 PGJyPnVzaW5nIEFuZHJvaWQuT1M7PGJyPnVzaW5nIEZyZXNoTXZ2bTs8YnI+dXNpbmcgRnJlc2hX aXRoU1FMaXRlLkNvcmU7PGJyPjxicj5uYW1lc3BhY2UgRnJlc2hXaXRoU1FMaXRlLkRyb2lkPGJy Pns8YnI+wqDCoMKgCiBbQWN0aXZpdHkoTGFiZWwgPSAiRnJlc2hXaXRoU1FMaXRlIiwgSWNvbiA9 ICJAZHJhd2FibGUvaWNvbiIsIFRoZW1lID0gCiJAc3R5bGUvTWFpblRoZW1lIiwgTWFpbkxhdW5j aGVyID0gdHJ1ZSwgQ29uZmlndXJhdGlvbkNoYW5nZXMgPSAKQ29uZmlnQ2hhbmdlcy5TY3JlZW5T aXplIHwgQ29uZmlnQ2hhbmdlcy5PcmllbnRhdGlvbildPGJyPsKgwqDCoCBwdWJsaWMgY2xhc3Mg TWFpbkFjdGl2aXR5IDogZ2xvYmFsOjpYYW1hcmluLkZvcm1zLlBsYXRmb3JtLkFuZHJvaWQuRm9y bXNBcHBDb21wYXRBY3Rpdml0eTxicj7CoMKgwqAgezxicj7CoMKgwqAgwqDCoMKgIHByb3RlY3Rl ZCBvdmVycmlkZSB2b2lkIE9uQ3JlYXRlKEJ1bmRsZSBidW5kbGUpPGJyPsKgwqDCoCDCoMKgwqAg ezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBUYWJMYXlvdXRSZXNvdXJjZSA9IFJlc291cmNlLkxh eW91dC5UYWJiYXI7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIFRvb2xiYXJSZXNvdXJjZSA9IFJl c291cmNlLkxheW91dC5Ub29sYmFyOzxicj48YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgYmFzZS5P bkNyZWF0ZShidW5kbGUpOzxicj48YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgZ2xvYmFsOjpYYW1h cmluLkZvcm1zLkZvcm1zLkluaXQodGhpcywgYnVuZGxlKTs8YnI+PGJyPsKgwqDCoCDCoMKgwqAg wqDCoMKgIHZhciByZXBvc2l0b3J5ID0gbmV3IFJlcG9zaXRvcnkoRmlsZUFjY2Vzc0hlbHBlci5H ZXRMb2NhbEZpbGVQYXRoKCJjb250YWN0cy5kYjMiKSk7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKg IEZyZXNoSU9DLkNvbnRhaW5lci5SZWdpc3RlcihyZXBvc2l0b3J5KTs8YnI+PGJyPsKgwqDCoCDC oMKgwqAgwqDCoMKgIExvYWRBcHBsaWNhdGlvbihuZXcgQXBwKCkpOzxicj7CoMKgwqAgwqDCoMKg IH08YnI+wqDCoMKgIH08YnI+fTxicj4KYGBg">​</div>
</div>
</div>
<div><span style="font-family:verdana,sans-serif"><br>
</span><span style="font-family:verdana,sans-serif"><b>FreshWithSQLite.iOS</b> <br>
<br>
</span></div>
<div><span style="font-family:verdana,sans-serif">Open the AppDelegate.cs class and add the following lines to the FinishedLaunching method, just before LoadApplication(new App()) is called:</span><br>
<span style="font-family:verdana,sans-serif">
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%">var repository = <span style="color:rgb(51,51,51);font-weight:bold">new</span> Repository(FileAccessHelper.GetLocalFilePath(<span style="color:rgb(221,17,68)">"contacts.db3"</span>));
FreshIOC.Container.Register(repository);
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnZhciByZXBvc2l0b3J5ID0gbmV3IFJlcG9zaXRvcnkoRmlsZUFjY2Vzc0hlbHBl ci5HZXRMb2NhbEZpbGVQYXRoKCJjb250YWN0cy5kYjMiKSk7PGJyPkZyZXNoSU9DLkNvbnRhaW5l ci5SZWdpc3RlcihyZXBvc2l0b3J5KTs8YnI+CmBgYA==">​</div>
</div>
</span></div>
<div><span style="font-family:verdana,sans-serif">The full AppDelegate.cs class should now look similar to this:</span><br>
<span style="font-family:verdana,sans-serif">
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> Foundation;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> UIKit;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshMvvm;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshWithSQLite.Core;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.iOS
{
    <span style="color:rgb(153,153,136);font-style:italic">// The UIApplicationDelegate for the application. This class is responsible for launching the </span>
    <span style="color:rgb(153,153,136);font-style:italic">// User Interface of the application, as well as listening (and optionally responding) to </span>
    <span style="color:rgb(153,153,136);font-style:italic">// application events from iOS.</span>
    [Register(<span style="color:rgb(221,17,68)">"AppDelegate"</span>)]
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> partial <span style="color:rgb(51,51,51);font-weight:bold">class</span> AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        <span style="color:rgb(153,153,136);font-style:italic">//</span>
        <span style="color:rgb(153,153,136);font-style:italic">// This method is invoked when the application has loaded and is ready to run. In this </span>
        <span style="color:rgb(153,153,136);font-style:italic">// method you should instantiate the window, load the UI into it and then make the window</span>
        <span style="color:rgb(153,153,136);font-style:italic">// visible.</span>
        <span style="color:rgb(153,153,136);font-style:italic">//</span>
        <span style="color:rgb(153,153,136);font-style:italic">// You have 17 seconds to return from this method, or iOS will terminate your application.</span>
        <span style="color:rgb(153,153,136);font-style:italic">//</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> override <span style="color:rgb(51,51,51);font-weight:bold">bool</span> FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            var repository = <span style="color:rgb(51,51,51);font-weight:bold">new</span> Repository(FileAccessHelper.GetLocalFilePath(<span style="color:rgb(221,17,68)">"contacts.db3"</span>));
            FreshIOC.Container.Register(repository);

            LoadApplication(<span style="color:rgb(51,51,51);font-weight:bold">new</span> App());

            <span style="color:rgb(51,51,51);font-weight:bold">return</span> base.FinishedLaunching(app, options);
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIEZvdW5kYXRpb247PGJyPnVzaW5nIFVJS2l0Ozxicj51c2luZyBGcmVz aE12dm07PGJyPnVzaW5nIEZyZXNoV2l0aFNRTGl0ZS5Db3JlOzxicj48YnI+bmFtZXNwYWNlIEZy ZXNoV2l0aFNRTGl0ZS5pT1M8YnI+ezxicj7CoMKgwqAgLy8gVGhlIFVJQXBwbGljYXRpb25EZWxl Z2F0ZSBmb3IgdGhlIGFwcGxpY2F0aW9uLiBUaGlzIGNsYXNzIGlzIHJlc3BvbnNpYmxlIGZvciBs YXVuY2hpbmcgdGhlIDxicj7CoMKgwqAgLy8gVXNlciBJbnRlcmZhY2Ugb2YgdGhlIGFwcGxpY2F0 aW9uLCBhcyB3ZWxsIGFzIGxpc3RlbmluZyAoYW5kIG9wdGlvbmFsbHkgcmVzcG9uZGluZykgdG8g PGJyPsKgwqDCoCAvLyBhcHBsaWNhdGlvbiBldmVudHMgZnJvbSBpT1MuPGJyPsKgwqDCoCBbUmVn aXN0ZXIoIkFwcERlbGVnYXRlIildPGJyPsKgwqDCoCBwdWJsaWMgcGFydGlhbCBjbGFzcyBBcHBE ZWxlZ2F0ZSA6IGdsb2JhbDo6WGFtYXJpbi5Gb3Jtcy5QbGF0Zm9ybS5pT1MuRm9ybXNBcHBsaWNh dGlvbkRlbGVnYXRlPGJyPsKgwqDCoCB7PGJyPsKgwqDCoCDCoMKgwqAgLy88YnI+wqDCoMKgIMKg wqDCoCAvLyBUaGlzIG1ldGhvZCBpcyBpbnZva2VkIHdoZW4gdGhlIGFwcGxpY2F0aW9uIGhhcyBs b2FkZWQgYW5kIGlzIHJlYWR5IHRvIHJ1bi4gSW4gdGhpcyA8YnI+wqDCoMKgIMKgwqDCoCAvLyBt ZXRob2QgeW91IHNob3VsZCBpbnN0YW50aWF0ZSB0aGUgd2luZG93LCBsb2FkIHRoZSBVSSBpbnRv IGl0IGFuZCB0aGVuIG1ha2UgdGhlIHdpbmRvdzxicj7CoMKgwqAgwqDCoMKgIC8vIHZpc2libGUu PGJyPsKgwqDCoCDCoMKgwqAgLy88YnI+wqDCoMKgIMKgwqDCoCAvLyBZb3UgaGF2ZSAxNyBzZWNv bmRzIHRvIHJldHVybiBmcm9tIHRoaXMgbWV0aG9kLCBvciBpT1Mgd2lsbCB0ZXJtaW5hdGUgeW91 ciBhcHBsaWNhdGlvbi48YnI+wqDCoMKgIMKgwqDCoCAvLzxicj7CoMKgwqAgwqDCoMKgIHB1Ymxp YyBvdmVycmlkZSBib29sIEZpbmlzaGVkTGF1bmNoaW5nKFVJQXBwbGljYXRpb24gYXBwLCBOU0Rp Y3Rpb25hcnkgb3B0aW9ucyk8YnI+wqDCoMKgIMKgwqDCoCB7PGJyPsKgwqDCoCDCoMKgwqAgwqDC oMKgIGdsb2JhbDo6WGFtYXJpbi5Gb3Jtcy5Gb3Jtcy5Jbml0KCk7PGJyPjxicj7CoMKgwqAgwqDC oMKgIMKgwqDCoCB2YXIgcmVwb3NpdG9yeSA9IG5ldyBSZXBvc2l0b3J5KEZpbGVBY2Nlc3NIZWxw ZXIuR2V0TG9jYWxGaWxlUGF0aCgiY29udGFjdHMuZGIzIikpOzxicj7CoMKgwqAgwqDCoMKgIMKg wqDCoCBGcmVzaElPQy5Db250YWluZXIuUmVnaXN0ZXIocmVwb3NpdG9yeSk7PGJyPjxicj7CoMKg wqAgwqDCoMKgIMKgwqDCoCBMb2FkQXBwbGljYXRpb24obmV3IEFwcCgpKTs8YnI+PGJyPsKgwqDC oCDCoMKgwqAgwqDCoMKgIHJldHVybiBiYXNlLkZpbmlzaGVkTGF1bmNoaW5nKGFwcCwgb3B0aW9u cyk7PGJyPsKgwqDCoCDCoMKgwqAgfTxicj7CoMKgwqAgfTxicj59PGJyPgpgYGA=">​</div>
</div>
<br>
</span><span style="font-family:verdana,sans-serif"><b>FreshWithSQLite.UPW<br>
<br>
</b>Open the MainPage.xaml.cs class and add the following lines to the constructor, just before LoadApplication(new FreshWithSQLite.App()) is called:</span><br>
<span style="font-family:verdana,sans-serif">
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%">var repository = <span style="color:rgb(51,51,51);font-weight:bold">new</span> Repository(FileAccessHelper.GetLocalFilePath(<span style="color:rgb(221,17,68)">"contacts.db3"</span>));
FreshIOC.Container.Register(repository);
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnZhciByZXBvc2l0b3J5ID0gbmV3IFJlcG9zaXRvcnkoRmlsZUFjY2Vzc0hlbHBl ci5HZXRMb2NhbEZpbGVQYXRoKCJjb250YWN0cy5kYjMiKSk7PGJyPkZyZXNoSU9DLkNvbnRhaW5l ci5SZWdpc3RlcihyZXBvc2l0b3J5KTs8YnI+CmBgYA=="><br>
</div>
</div>
</span></div>
<div>
<p><font face="verdana,sans-serif">The full MainPage.xaml.cs class should now look similar to this:</font><br>
</p>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshMvvm;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshWithSQLite.Core;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.UWP
{
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> sealed partial <span style="color:rgb(51,51,51);font-weight:bold">class</span> MainPage
    {
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> MainPage()
        {
            <span style="color:rgb(51,51,51);font-weight:bold">this</span>.InitializeComponent();

            var repository = <span style="color:rgb(51,51,51);font-weight:bold">new</span> Repository(FileAccessHelper.GetLocalFilePath(<span style="color:rgb(221,17,68)">"contacts.db3"</span>));
            FreshIOC.Container.Register(repository);

            LoadApplication(<span style="color:rgb(51,51,51);font-weight:bold">new</span> FreshWithSQLite.App());
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIEZyZXNoTXZ2bTs8YnI+dXNpbmcgRnJlc2hXaXRoU1FMaXRlLkNvcmU7 PGJyPjxicj5uYW1lc3BhY2UgRnJlc2hXaXRoU1FMaXRlLlVXUDxicj57PGJyPsKgwqDCoCBwdWJs aWMgc2VhbGVkIHBhcnRpYWwgY2xhc3MgTWFpblBhZ2U8YnI+wqDCoMKgIHs8YnI+wqDCoMKgIMKg wqDCoCBwdWJsaWMgTWFpblBhZ2UoKTxicj7CoMKgwqAgwqDCoMKgIHs8YnI+wqDCoMKgIMKgwqDC oCDCoMKgwqAgdGhpcy5Jbml0aWFsaXplQ29tcG9uZW50KCk7PGJyPjxicj7CoMKgwqAgwqDCoMKg IMKgwqDCoCB2YXIgcmVwb3NpdG9yeSA9IG5ldyBSZXBvc2l0b3J5KEZpbGVBY2Nlc3NIZWxwZXIu R2V0TG9jYWxGaWxlUGF0aCgiY29udGFjdHMuZGIzIikpOzxicj7CoMKgwqAgwqDCoMKgIMKgwqDC oCBGcmVzaElPQy5Db250YWluZXIuUmVnaXN0ZXIocmVwb3NpdG9yeSk7PGJyPjxicj7CoMKgwqAg wqDCoMKgIMKgwqDCoCBMb2FkQXBwbGljYXRpb24obmV3IEZyZXNoV2l0aFNRTGl0ZS5BcHAoKSk7 PGJyPsKgwqDCoCDCoMKgwqAgfTxicj7CoMKgwqAgfTxicj59PGJyPgpgYGA=">​</div>
</div>
<hr>
<h2>Create the Contact page<br>
</h2>
<h3><a name="TOC-Overview"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif"><span style="font-family:verdana,sans-serif">We
 have successfully implemented our Model and SQLite repository, now it 
is time to start creating our Pages and Page Models.&nbsp; The first page we will create will allow us to edit an individual Contact.</span>&nbsp; In this section we will create the ContactPage XAML page, and the ContactPageModel that drives it.<br>
</span></p>
<h3><a name="TOC-Create-a-FileAccessHelper-in-each-UI-project"></a><span style="font-family:verdana,sans-serif">Steps</span></h3>
<p><font face="verdana,sans-serif">Create a new folder called PageModels in the PCL.&nbsp; Inside this folder, create a new class called ContactPageModel with the following code (see code comments for descriptions):</font><br>
</p>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshMvvm;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshWithSQLite.Core;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshWithSQLite.Models;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> System.Windows.Input;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Xamarin.Forms;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.PageModels
{
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> ContactPageModel : FreshBasePageModel
    {
        <span style="color:rgb(153,153,136);font-style:italic">// Use IoC to get our repository.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">private</span> Repository _repository = FreshIOC.Container.Resolve&lt;Repository&gt;();

        <span style="color:rgb(153,153,136);font-style:italic">// Backing data model.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">private</span> Contact _contact;

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Public property exposing the contact's name for Page binding.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(0,134,179)">string</span> ContactName{
            get { <span style="color:rgb(51,51,51);font-weight:bold">return</span> _contact.Name; }
            <span style="color:rgb(0,134,179)">set</span> { _contact.Name = value; RaisePropertyChanged(); }
        }

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Public property exposing the contact's email for Page binding.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(0,134,179)">string</span> ContactEmail
        {
            get { <span style="color:rgb(51,51,51);font-weight:bold">return</span> _contact.Email; }
            <span style="color:rgb(0,134,179)">set</span> { _contact.Email = value;  RaisePropertyChanged(); }
        }

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Called whenever the page is navigated to.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Either use a supplied Contact, or create a new one if not supplied.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// FreshMVVM does not provide a RaiseAllPropertyChanged,</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// so we do this for each bound property, room for improvement.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> override <span style="color:rgb(51,51,51);font-weight:bold">void</span> Init(object initData)
        {
            _contact = initData as Contact;
            <span style="color:rgb(51,51,51);font-weight:bold">if</span>(_contact == null) _contact = <span style="color:rgb(51,51,51);font-weight:bold">new</span> Contact();
            base.Init(initData);
            RaisePropertyChanged(nameof(ContactName));
            RaisePropertyChanged(nameof(ContactEmail));
        }

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Command associated with the save action.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Persists the contact to the database if the contact is valid.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> ICommand SaveCommand
        {
            get {
                <span style="color:rgb(51,51,51);font-weight:bold">return</span> <span style="color:rgb(51,51,51);font-weight:bold">new</span> Command(async () =&gt; {
                    <span style="color:rgb(51,51,51);font-weight:bold">if</span>(_contact.IsValid()) {
                        await _repository.CreateContact(_contact);
                        await CoreMethods.PopPageModel(_contact);
                    }
                });
            }
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIEZyZXNoTXZ2bTs8YnI+dXNpbmcgRnJlc2hXaXRoU1FMaXRlLkNvcmU7 PGJyPnVzaW5nIEZyZXNoV2l0aFNRTGl0ZS5Nb2RlbHM7PGJyPnVzaW5nIFN5c3RlbS5XaW5kb3dz LklucHV0Ozxicj51c2luZyBYYW1hcmluLkZvcm1zOzxicj48YnI+bmFtZXNwYWNlIEZyZXNoV2l0 aFNRTGl0ZS5QYWdlTW9kZWxzPGJyPns8YnI+wqDCoMKgIHB1YmxpYyBjbGFzcyBDb250YWN0UGFn ZU1vZGVsIDogRnJlc2hCYXNlUGFnZU1vZGVsPGJyPsKgwqDCoCB7PGJyPsKgwqDCoCDCoMKgwqAg Ly8gVXNlIElvQyB0byBnZXQgb3VyIHJlcG9zaXRvcnkuPGJyPsKgwqDCoCDCoMKgwqAgcHJpdmF0 ZSBSZXBvc2l0b3J5IF9yZXBvc2l0b3J5ID0gRnJlc2hJT0MuQ29udGFpbmVyLlJlc29sdmUmbHQ7 UmVwb3NpdG9yeSZndDsoKTs8YnI+PGJyPsKgwqDCoCDCoMKgwqAgLy8gQmFja2luZyBkYXRhIG1v ZGVsLjxicj7CoMKgwqAgwqDCoMKgIHByaXZhdGUgQ29udGFjdCBfY29udGFjdDs8YnI+PGJyPsKg wqDCoCDCoMKgwqAgLy8vICZsdDtzdW1tYXJ5Jmd0Ozxicj7CoMKgwqAgwqDCoMKgIC8vLyBQdWJs aWMgcHJvcGVydHkgZXhwb3NpbmcgdGhlIGNvbnRhY3QncyBuYW1lIGZvciBQYWdlIGJpbmRpbmcu PGJyPsKgwqDCoCDCoMKgwqAgLy8vICZsdDsvc3VtbWFyeSZndDs8YnI+wqDCoMKgIMKgwqDCoCBw dWJsaWMgc3RyaW5nIENvbnRhY3ROYW1lezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBnZXQgeyBy ZXR1cm4gX2NvbnRhY3QuTmFtZTsgfTxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBzZXQgeyBfY29u dGFjdC5OYW1lID0gdmFsdWU7IFJhaXNlUHJvcGVydHlDaGFuZ2VkKCk7IH08YnI+wqDCoMKgIMKg wqDCoCB9PGJyPjxicj7CoMKgwqAgwqDCoMKgIC8vLyAmbHQ7c3VtbWFyeSZndDs8YnI+wqDCoMKg IMKgwqDCoCAvLy8gUHVibGljIHByb3BlcnR5IGV4cG9zaW5nIHRoZSBjb250YWN0J3MgZW1haWwg Zm9yIFBhZ2UgYmluZGluZy48YnI+wqDCoMKgIMKgwqDCoCAvLy8gJmx0Oy9zdW1tYXJ5Jmd0Ozxi cj7CoMKgwqAgwqDCoMKgIHB1YmxpYyBzdHJpbmcgQ29udGFjdEVtYWlsPGJyPsKgwqDCoCDCoMKg wqAgezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBnZXQgeyByZXR1cm4gX2NvbnRhY3QuRW1haWw7 IH08YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgc2V0IHsgX2NvbnRhY3QuRW1haWwgPSB2YWx1ZTvC oCBSYWlzZVByb3BlcnR5Q2hhbmdlZCgpOyB9PGJyPsKgwqDCoCDCoMKgwqAgfTxicj48YnI+wqDC oMKgIMKgwqDCoCAvLy8gJmx0O3N1bW1hcnkmZ3Q7PGJyPsKgwqDCoCDCoMKgwqAgLy8vIENhbGxl ZCB3aGVuZXZlciB0aGUgcGFnZSBpcyBuYXZpZ2F0ZWQgdG8uPGJyPsKgwqDCoCDCoMKgwqAgLy8v IEVpdGhlciB1c2UgYSBzdXBwbGllZCBDb250YWN0LCBvciBjcmVhdGUgYSBuZXcgb25lIGlmIG5v dCBzdXBwbGllZC48YnI+wqDCoMKgIMKgwqDCoCAvLy8gRnJlc2hNVlZNIGRvZXMgbm90IHByb3Zp ZGUgYSBSYWlzZUFsbFByb3BlcnR5Q2hhbmdlZCw8YnI+wqDCoMKgIMKgwqDCoCAvLy8gc28gd2Ug ZG8gdGhpcyBmb3IgZWFjaCBib3VuZCBwcm9wZXJ0eSwgcm9vbSBmb3IgaW1wcm92ZW1lbnQuPGJy PsKgwqDCoCDCoMKgwqAgLy8vICZsdDsvc3VtbWFyeSZndDs8YnI+wqDCoMKgIMKgwqDCoCBwdWJs aWMgb3ZlcnJpZGUgdm9pZCBJbml0KG9iamVjdCBpbml0RGF0YSk8YnI+wqDCoMKgIMKgwqDCoCB7 PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIF9jb250YWN0ID0gaW5pdERhdGEgYXMgQ29udGFjdDs8 YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgaWYoX2NvbnRhY3QgPT0gbnVsbCkgX2NvbnRhY3QgPSBu ZXcgQ29udGFjdCgpOzxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBiYXNlLkluaXQoaW5pdERhdGEp Ozxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBSYWlzZVByb3BlcnR5Q2hhbmdlZChuYW1lb2YoQ29u dGFjdE5hbWUpKTs8YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgUmFpc2VQcm9wZXJ0eUNoYW5nZWQo bmFtZW9mKENvbnRhY3RFbWFpbCkpOzxicj7CoMKgwqAgwqDCoMKgIH08YnI+PGJyPsKgwqDCoCDC oMKgwqAgLy8vICZsdDtzdW1tYXJ5Jmd0Ozxicj7CoMKgwqAgwqDCoMKgIC8vLyBDb21tYW5kIGFz c29jaWF0ZWQgd2l0aCB0aGUgc2F2ZSBhY3Rpb24uPGJyPsKgwqDCoCDCoMKgwqAgLy8vIFBlcnNp c3RzIHRoZSBjb250YWN0IHRvIHRoZSBkYXRhYmFzZSBpZiB0aGUgY29udGFjdCBpcyB2YWxpZC48 YnI+wqDCoMKgIMKgwqDCoCAvLy8gJmx0Oy9zdW1tYXJ5Jmd0Ozxicj7CoMKgwqAgwqDCoMKgIHB1 YmxpYyBJQ29tbWFuZCBTYXZlQ29tbWFuZDxicj7CoMKgwqAgwqDCoMKgIHs8YnI+wqDCoMKgIMKg wqDCoCDCoMKgwqAgZ2V0IHs8YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgwqDCoMKgIHJldHVybiBu ZXcgQ29tbWFuZChhc3luYyAoKSA9Jmd0OyB7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIMKgwqDC oCDCoMKgwqAgaWYoX2NvbnRhY3QuSXNWYWxpZCgpKSB7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKg IMKgwqDCoCDCoMKgwqAgwqDCoMKgIGF3YWl0IF9yZXBvc2l0b3J5LkNyZWF0ZUNvbnRhY3QoX2Nv bnRhY3QpOzxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDCoMKgwqAgwqDCoMKgIMKgwqDCoCBhd2Fp dCBDb3JlTWV0aG9kcy5Qb3BQYWdlTW9kZWwoX2NvbnRhY3QpOzxicj7CoMKgwqAgwqDCoMKgIMKg wqDCoCDCoMKgwqAgwqDCoMKgIH08YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgwqDCoMKgIH0pOzxi cj7CoMKgwqAgwqDCoMKgIMKgwqDCoCB9PGJyPsKgwqDCoCDCoMKgwqAgfTxicj7CoMKgwqAgfTxi cj59PGJyPgpgYGA=">​</div>
</div>
<p><font face="verdana,sans-serif">Create a new folder called Pages in the PCL.&nbsp; </font><font face="verdana,sans-serif">Inside this folder, create a new 'Forms Xaml Page' called ContactPage with the following code (see code comments for descriptions):</font></p>
<p><font face="verdana,sans-serif"><b>ContactPage.xaml</b></font></p>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(153,153,153);font-weight:bold">&lt;?xml version="1.0" encoding="utf-8" ?&gt;</span>
<span style="color:rgb(153,153,136);font-style:italic">&lt;!-- Add the xmlns:fresh line and use it to resolve the fresh:FreshBaseContentPage declaration --&gt;</span>
<span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">fresh:FreshBaseContentPage</span> <span style="color:rgb(0,128,128)">xmlns</span>=<span style="color:rgb(221,17,68)">"http://xamarin.com/schemas/2014/forms"</span>
                            <span style="color:rgb(0,128,128)">xmlns:x</span>=<span style="color:rgb(221,17,68)">"http://schemas.microsoft.com/winfx/2009/xaml"</span>
                            <span style="color:rgb(0,128,128)">x:Class</span>=<span style="color:rgb(221,17,68)">"FreshWithSQLite.Pages.ContactPage"</span>
                            <span style="color:rgb(0,128,128)">xmlns:fresh</span>=<span style="color:rgb(221,17,68)">"clr-namespace:FreshMvvm;assembly=FreshWithSQLite"</span>&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">ContentPage.Content</span>&gt;</span>
    <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">StackLayout</span> <span style="color:rgb(0,128,128)">Padding</span>=<span style="color:rgb(221,17,68)">"15"</span> <span style="color:rgb(0,128,128)">Spacing</span>=<span style="color:rgb(221,17,68)">"5"</span>&gt;</span>
      <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">Label</span> <span style="color:rgb(0,128,128)">Text</span>=<span style="color:rgb(221,17,68)">"Contact Name"</span> /&gt;</span>
      <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">Entry</span> <span style="color:rgb(0,128,128)">Text</span>=<span style="color:rgb(221,17,68)">"{Binding ContactName}"</span> /&gt;</span>
      <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">Label</span> <span style="color:rgb(0,128,128)">Text</span>=<span style="color:rgb(221,17,68)">"Contact Email"</span> /&gt;</span>
      <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">Entry</span> <span style="color:rgb(0,128,128)">Text</span>=<span style="color:rgb(221,17,68)">"{Binding ContactEmail}"</span> /&gt;</span>
      <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">Button</span> <span style="color:rgb(0,128,128)">Text</span>=<span style="color:rgb(221,17,68)">"Save"</span> <span style="color:rgb(0,128,128)">Command</span>=<span style="color:rgb(221,17,68)">"{Binding SaveCommand}"</span> /&gt;</span>
    <span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">StackLayout</span>&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">ContentPage.Content</span>&gt;</span>
<span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">fresh:FreshBaseContentPage</span>&gt;</span>
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgWE1MPGJyPiZsdDs/eG1sIHZlcnNpb249IjEuMCIgZW5jb2Rpbmc9InV0Zi04IiA/Jmd0Ozxi cj4mbHQ7IS0tIEFkZCB0aGUgeG1sbnM6ZnJlc2ggbGluZSBhbmQgdXNlIGl0IHRvIHJlc29sdmUg dGhlIGZyZXNoOkZyZXNoQmFzZUNvbnRlbnRQYWdlIGRlY2xhcmF0aW9uIC0tJmd0Ozxicj4mbHQ7 ZnJlc2g6RnJlc2hCYXNlQ29udGVudFBhZ2UgeG1sbnM9Imh0dHA6Ly94YW1hcmluLmNvbS9zY2hl bWFzLzIwMTQvZm9ybXMiPGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoCB4bWxuczp4PSJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dp bmZ4LzIwMDkveGFtbCI8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgIHg6Q2xhc3M9IkZyZXNoV2l0aFNRTGl0ZS5QYWdlcy5Db250YWN0UGFn ZSI8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgIHhtbG5zOmZyZXNoPSJjbHItbmFtZXNwYWNlOkZyZXNoTXZ2bTthc3NlbWJseT1GcmVzaFdp dGhTUUxpdGUiJmd0Ozxicj7CoCAmbHQ7Q29udGVudFBhZ2UuQ29udGVudCZndDs8YnI+wqDCoMKg ICZsdDtTdGFja0xheW91dCBQYWRkaW5nPSIxNSIgU3BhY2luZz0iNSImZ3Q7PGJyPsKgwqDCoMKg wqAgJmx0O0xhYmVsIFRleHQ9IkNvbnRhY3QgTmFtZSIgLyZndDs8YnI+wqDCoMKgwqDCoCAmbHQ7 RW50cnkgVGV4dD0ie0JpbmRpbmcgQ29udGFjdE5hbWV9IiAvJmd0Ozxicj7CoMKgwqDCoMKgICZs dDtMYWJlbCBUZXh0PSJDb250YWN0IEVtYWlsIiAvJmd0Ozxicj7CoMKgwqDCoMKgICZsdDtFbnRy eSBUZXh0PSJ7QmluZGluZyBDb250YWN0RW1haWx9IiAvJmd0Ozxicj7CoMKgwqDCoMKgICZsdDtC dXR0b24gVGV4dD0iU2F2ZSIgQ29tbWFuZD0ie0JpbmRpbmcgU2F2ZUNvbW1hbmR9IiAvJmd0Ozxi cj7CoMKgwqAgJmx0Oy9TdGFja0xheW91dCZndDs8YnI+wqAgJmx0Oy9Db250ZW50UGFnZS5Db250 ZW50Jmd0Ozxicj4mbHQ7L2ZyZXNoOkZyZXNoQmFzZUNvbnRlbnRQYWdlJmd0Ozxicj4KYGBg">​</div>
</div>
<b><font face="verdana,sans-serif">ContactPage.xaml.cs</font></b>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshMvvm;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.Pages
{
    <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
    <span style="color:rgb(153,153,136);font-style:italic">/// Update class to inherit from FreshBaseContentPage</span>
    <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> partial <span style="color:rgb(51,51,51);font-weight:bold">class</span> ContactPage : FreshBaseContentPage
    {
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> ContactPage()
        {
            InitializeComponent();
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIEZyZXNoTXZ2bTs8YnI+PGJyPm5hbWVzcGFjZSBGcmVzaFdpdGhTUUxp dGUuUGFnZXM8YnI+ezxicj7CoMKgwqAgLy8vICZsdDtzdW1tYXJ5Jmd0Ozxicj7CoMKgwqAgLy8v IFVwZGF0ZSBjbGFzcyB0byBpbmhlcml0IGZyb20gRnJlc2hCYXNlQ29udGVudFBhZ2U8YnI+wqDC oMKgIC8vLyAmbHQ7L3N1bW1hcnkmZ3Q7PGJyPsKgwqDCoCBwdWJsaWMgcGFydGlhbCBjbGFzcyBD b250YWN0UGFnZSA6IEZyZXNoQmFzZUNvbnRlbnRQYWdlPGJyPsKgwqDCoCB7PGJyPsKgwqDCoCDC oMKgwqAgcHVibGljIENvbnRhY3RQYWdlKCk8YnI+wqDCoMKgIMKgwqDCoCB7PGJyPsKgwqDCoCDC oMKgwqAgwqDCoMKgIEluaXRpYWxpemVDb21wb25lbnQoKTs8YnI+wqDCoMKgIMKgwqDCoCB9PGJy PsKgwqDCoCB9PGJyPn08YnI+CmBgYA==">​</div>
</div>
<hr>
<h2>Create the Contact List page<br>
</h2>
<h3><a name="TOC-Overview"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif"><span style="font-family:verdana,sans-serif">Now that we've created the Contact page, we will create a page to view all our user's existing Contacts.&nbsp; This page will make use of the Contact page for updates and edits.</span><br>
</span></p>
<h3><a name="TOC-Create-a-FileAccessHelper-in-each-UI-project"></a><span style="font-family:verdana,sans-serif">Steps</span></h3>
<font face="verdana,sans-serif">In the PCL, inside the PageModels folder, create a
 new class called ContactListPageModel with the following code (see code 
comments for descriptions):</font><br>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshMvvm;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshWithSQLite.Core;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshWithSQLite.Models;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> System.Collections.Generic;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> System.Collections.ObjectModel;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> System.Linq;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> System.Threading.Tasks;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> System.Windows.Input;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Xamarin.Forms;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.PageModels
{
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> ContactListPageModel : FreshBasePageModel
    {
        <span style="color:rgb(51,51,51);font-weight:bold">private</span> Repository _repository = FreshIOC.Container.Resolve&lt;Repository&gt;();
        <span style="color:rgb(51,51,51);font-weight:bold">private</span> Contact _selectedContact = null;

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Collection used for binding to the Page's contact list view.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> ObservableCollection&lt;Contact&gt; Contacts { get; <span style="color:rgb(51,51,51);font-weight:bold">private</span> <span style="color:rgb(0,134,179)">set</span>; }

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Used to bind with the list view's SelectedItem property.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Calls the EditContactCommand to start the editing.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> Contact SelectedContact
        {
            get { <span style="color:rgb(51,51,51);font-weight:bold">return</span> _selectedContact; }
            <span style="color:rgb(0,134,179)">set</span> {
                _selectedContact = value;
                <span style="color:rgb(51,51,51);font-weight:bold">if</span>(value != null) EditContactCommand.Execute(value);
            }
        }

        <span style="color:rgb(51,51,51);font-weight:bold">public</span> ContactListPageModel()
        {
            Contacts = <span style="color:rgb(51,51,51);font-weight:bold">new</span> ObservableCollection&lt;Contact&gt;();
        }

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Called whenever the page is navigated to.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Here we are ignoring the init data and just loading the contacts.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> override <span style="color:rgb(51,51,51);font-weight:bold">void</span> Init(object initData)
        {
            LoadContacts();
            <span style="color:rgb(51,51,51);font-weight:bold">if</span>(Contacts.Count() &lt; <span style="color:rgb(0,128,128)">1</span>) {
                CreateSampleData();
            }
        }

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Called whenever the page is navigated to, but from a pop action.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Here we are just updating the contact list with most recent data.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;param name="returnedData"&gt;&lt;/param&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> override <span style="color:rgb(51,51,51);font-weight:bold">void</span> ReverseInit(object returnedData)
        {
            LoadContacts();
            base.ReverseInit(returnedData);
        }

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Command associated with the add contact action.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Navigates to the ContactPageModel with no Init object.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> ICommand AddContactCommand
        {
            get {
                <span style="color:rgb(51,51,51);font-weight:bold">return</span> <span style="color:rgb(51,51,51);font-weight:bold">new</span> Command(async () =&gt; {
                    await CoreMethods.PushPageModel&lt;ContactPageModel&gt;();
                });
            }
        }

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Command associated with the edit contact action.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Navigates to the ContactPageModel with the selected contact as the Init object.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> ICommand EditContactCommand
        {
            get {
                <span style="color:rgb(51,51,51);font-weight:bold">return</span> <span style="color:rgb(51,51,51);font-weight:bold">new</span> Command(async (contact) =&gt; {
                    await CoreMethods.PushPageModel&lt;ContactPageModel&gt;(contact);
                });
            }
        }

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Repopulate the collection with updated contacts data.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Note: For simplicity, we wait for the async db call to complete,</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// recommend making better use of the async potential.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">private</span> <span style="color:rgb(51,51,51);font-weight:bold">void</span> LoadContacts()
        {
            Contacts.Clear();
            Task&lt;List&lt;Contact&gt;&gt; getContactsTask = _repository.GetAllContacts();
            getContactsTask.Wait();
            foreach(var contact in getContactsTask.Result) {
                Contacts.Add(contact);
            }
        }

        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// Uses the SQLite Async capability to insert sample data on multiple threads.</span>
        <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
        <span style="color:rgb(51,51,51);font-weight:bold">private</span> <span style="color:rgb(51,51,51);font-weight:bold">void</span> CreateSampleData()
        {
            var contact1 = <span style="color:rgb(51,51,51);font-weight:bold">new</span> Contact {
                Name = <span style="color:rgb(221,17,68)">"Jake Smith"</span>,
                Email = <span style="color:rgb(221,17,68)">"jake.smith@mailmail.com"</span>
            };

            var contact2 = <span style="color:rgb(51,51,51);font-weight:bold">new</span> Contact {
                Name = <span style="color:rgb(221,17,68)">"Jane Smith"</span>,
                Email = <span style="color:rgb(221,17,68)">"jane.smith@mailmail.com"</span>
            };

            var contact3 = <span style="color:rgb(51,51,51);font-weight:bold">new</span> Contact {
                Name = <span style="color:rgb(221,17,68)">"Jim Bob"</span>,
                Email = <span style="color:rgb(221,17,68)">"jim.bob@mailmail.com"</span>
            };

            var task1 = _repository.CreateContact(contact1);
            var task2 = _repository.CreateContact(contact2);
            var task3 = _repository.CreateContact(contact3);

            <span style="color:rgb(153,153,136);font-style:italic">// Don't proceed until all the async inserts are complete.</span>
            var allTasks = Task.WhenAll(task1, task2, task3);
            allTasks.Wait();

            LoadContacts();
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIEZyZXNoTXZ2bTs8YnI+dXNpbmcgRnJlc2hXaXRoU1FMaXRlLkNvcmU7 PGJyPnVzaW5nIEZyZXNoV2l0aFNRTGl0ZS5Nb2RlbHM7PGJyPnVzaW5nIFN5c3RlbS5Db2xsZWN0 aW9ucy5HZW5lcmljOzxicj51c2luZyBTeXN0ZW0uQ29sbGVjdGlvbnMuT2JqZWN0TW9kZWw7PGJy PnVzaW5nIFN5c3RlbS5MaW5xOzxicj51c2luZyBTeXN0ZW0uVGhyZWFkaW5nLlRhc2tzOzxicj51 c2luZyBTeXN0ZW0uV2luZG93cy5JbnB1dDs8YnI+dXNpbmcgWGFtYXJpbi5Gb3Jtczs8YnI+PGJy Pm5hbWVzcGFjZSBGcmVzaFdpdGhTUUxpdGUuUGFnZU1vZGVsczxicj57PGJyPsKgwqDCoCBwdWJs aWMgY2xhc3MgQ29udGFjdExpc3RQYWdlTW9kZWwgOiBGcmVzaEJhc2VQYWdlTW9kZWw8YnI+wqDC oMKgIHs8YnI+wqDCoMKgIMKgwqDCoCBwcml2YXRlIFJlcG9zaXRvcnkgX3JlcG9zaXRvcnkgPSBG cmVzaElPQy5Db250YWluZXIuUmVzb2x2ZSZsdDtSZXBvc2l0b3J5Jmd0OygpOzxicj7CoMKgwqAg wqDCoMKgIHByaXZhdGUgQ29udGFjdCBfc2VsZWN0ZWRDb250YWN0ID0gbnVsbDs8YnI+PGJyPsKg wqDCoCDCoMKgwqAgLy8vICZsdDtzdW1tYXJ5Jmd0Ozxicj7CoMKgwqAgwqDCoMKgIC8vLyBDb2xs ZWN0aW9uIHVzZWQgZm9yIGJpbmRpbmcgdG8gdGhlIFBhZ2UncyBjb250YWN0IGxpc3Qgdmlldy48 YnI+wqDCoMKgIMKgwqDCoCAvLy8gJmx0Oy9zdW1tYXJ5Jmd0Ozxicj7CoMKgwqAgwqDCoMKgIHB1 YmxpYyBPYnNlcnZhYmxlQ29sbGVjdGlvbiZsdDtDb250YWN0Jmd0OyBDb250YWN0cyB7IGdldDsg cHJpdmF0ZSBzZXQ7IH08YnI+PGJyPsKgwqDCoCDCoMKgwqAgLy8vICZsdDtzdW1tYXJ5Jmd0Ozxi cj7CoMKgwqAgwqDCoMKgIC8vLyBVc2VkIHRvIGJpbmQgd2l0aCB0aGUgbGlzdCB2aWV3J3MgU2Vs ZWN0ZWRJdGVtIHByb3BlcnR5Ljxicj7CoMKgwqAgwqDCoMKgIC8vLyBDYWxscyB0aGUgRWRpdENv bnRhY3RDb21tYW5kIHRvIHN0YXJ0IHRoZSBlZGl0aW5nLjxicj7CoMKgwqAgwqDCoMKgIC8vLyAm bHQ7L3N1bW1hcnkmZ3Q7PGJyPsKgwqDCoCDCoMKgwqAgcHVibGljIENvbnRhY3QgU2VsZWN0ZWRD b250YWN0PGJyPsKgwqDCoCDCoMKgwqAgezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBnZXQgeyBy ZXR1cm4gX3NlbGVjdGVkQ29udGFjdDsgfTxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBzZXQgezxi cj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDCoMKgwqAgX3NlbGVjdGVkQ29udGFjdCA9IHZhbHVlOzxi cj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDCoMKgwqAgaWYodmFsdWUgIT0gbnVsbCkgRWRpdENvbnRh Y3RDb21tYW5kLkV4ZWN1dGUodmFsdWUpOzxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCB9PGJyPsKg wqDCoCDCoMKgwqAgfTxicj48YnI+wqDCoMKgIMKgwqDCoCBwdWJsaWMgQ29udGFjdExpc3RQYWdl TW9kZWwoKTxicj7CoMKgwqAgwqDCoMKgIHs8YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgQ29udGFj dHMgPSBuZXcgT2JzZXJ2YWJsZUNvbGxlY3Rpb24mbHQ7Q29udGFjdCZndDsoKTs8YnI+wqDCoMKg IMKgwqDCoCB9PGJyPjxicj7CoMKgwqAgwqDCoMKgIC8vLyAmbHQ7c3VtbWFyeSZndDs8YnI+wqDC oMKgIMKgwqDCoCAvLy8gQ2FsbGVkIHdoZW5ldmVyIHRoZSBwYWdlIGlzIG5hdmlnYXRlZCB0by48 YnI+wqDCoMKgIMKgwqDCoCAvLy8gSGVyZSB3ZSBhcmUgaWdub3JpbmcgdGhlIGluaXQgZGF0YSBh bmQganVzdCBsb2FkaW5nIHRoZSBjb250YWN0cy48YnI+wqDCoMKgIMKgwqDCoCAvLy8gJmx0Oy9z dW1tYXJ5Jmd0Ozxicj7CoMKgwqAgwqDCoMKgIHB1YmxpYyBvdmVycmlkZSB2b2lkIEluaXQob2Jq ZWN0IGluaXREYXRhKTxicj7CoMKgwqAgwqDCoMKgIHs8YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAg TG9hZENvbnRhY3RzKCk7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIGlmKENvbnRhY3RzLkNvdW50 KCkgJmx0OyAxKSB7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIMKgwqDCoCBDcmVhdGVTYW1wbGVE YXRhKCk7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIH08YnI+wqDCoMKgIMKgwqDCoCB9PGJyPjxi cj7CoMKgwqAgwqDCoMKgIC8vLyAmbHQ7c3VtbWFyeSZndDs8YnI+wqDCoMKgIMKgwqDCoCAvLy8g Q2FsbGVkIHdoZW5ldmVyIHRoZSBwYWdlIGlzIG5hdmlnYXRlZCB0bywgYnV0IGZyb20gYSBwb3Ag YWN0aW9uLjxicj7CoMKgwqAgwqDCoMKgIC8vLyBIZXJlIHdlIGFyZSBqdXN0IHVwZGF0aW5nIHRo ZSBjb250YWN0IGxpc3Qgd2l0aCBtb3N0IHJlY2VudCBkYXRhLjxicj7CoMKgwqAgwqDCoMKgIC8v LyAmbHQ7L3N1bW1hcnkmZ3Q7PGJyPsKgwqDCoCDCoMKgwqAgLy8vICZsdDtwYXJhbSBuYW1lPSJy ZXR1cm5lZERhdGEiJmd0OyZsdDsvcGFyYW0mZ3Q7PGJyPsKgwqDCoCDCoMKgwqAgcHVibGljIG92 ZXJyaWRlIHZvaWQgUmV2ZXJzZUluaXQob2JqZWN0IHJldHVybmVkRGF0YSk8YnI+wqDCoMKgIMKg wqDCoCB7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIExvYWRDb250YWN0cygpOzxicj7CoMKgwqAg wqDCoMKgIMKgwqDCoCBiYXNlLlJldmVyc2VJbml0KHJldHVybmVkRGF0YSk7PGJyPsKgwqDCoCDC oMKgwqAgfTxicj48YnI+wqDCoMKgIMKgwqDCoCAvLy8gJmx0O3N1bW1hcnkmZ3Q7PGJyPsKgwqDC oCDCoMKgwqAgLy8vIENvbW1hbmQgYXNzb2NpYXRlZCB3aXRoIHRoZSBhZGQgY29udGFjdCBhY3Rp b24uPGJyPsKgwqDCoCDCoMKgwqAgLy8vIE5hdmlnYXRlcyB0byB0aGUgQ29udGFjdFBhZ2VNb2Rl bCB3aXRoIG5vIEluaXQgb2JqZWN0Ljxicj7CoMKgwqAgwqDCoMKgIC8vLyAmbHQ7L3N1bW1hcnkm Z3Q7PGJyPsKgwqDCoCDCoMKgwqAgcHVibGljIElDb21tYW5kIEFkZENvbnRhY3RDb21tYW5kPGJy PsKgwqDCoCDCoMKgwqAgezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBnZXQgezxicj7CoMKgwqAg wqDCoMKgIMKgwqDCoCDCoMKgwqAgcmV0dXJuIG5ldyBDb21tYW5kKGFzeW5jICgpID0mZ3Q7IHs8 YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgwqDCoMKgIMKgwqDCoCBhd2FpdCBDb3JlTWV0aG9kcy5Q dXNoUGFnZU1vZGVsJmx0O0NvbnRhY3RQYWdlTW9kZWwmZ3Q7KCk7PGJyPsKgwqDCoCDCoMKgwqAg wqDCoMKgIMKgwqDCoCB9KTs8YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgfTxicj7CoMKgwqAgwqDC oMKgIH08YnI+PGJyPsKgwqDCoCDCoMKgwqAgLy8vICZsdDtzdW1tYXJ5Jmd0Ozxicj7CoMKgwqAg wqDCoMKgIC8vLyBDb21tYW5kIGFzc29jaWF0ZWQgd2l0aCB0aGUgZWRpdCBjb250YWN0IGFjdGlv bi48YnI+wqDCoMKgIMKgwqDCoCAvLy8gTmF2aWdhdGVzIHRvIHRoZSBDb250YWN0UGFnZU1vZGVs IHdpdGggdGhlIHNlbGVjdGVkIGNvbnRhY3QgYXMgdGhlIEluaXQgb2JqZWN0Ljxicj7CoMKgwqAg wqDCoMKgIC8vLyAmbHQ7L3N1bW1hcnkmZ3Q7PGJyPsKgwqDCoCDCoMKgwqAgcHVibGljIElDb21t YW5kIEVkaXRDb250YWN0Q29tbWFuZDxicj7CoMKgwqAgwqDCoMKgIHs8YnI+wqDCoMKgIMKgwqDC oCDCoMKgwqAgZ2V0IHs8YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgwqDCoMKgIHJldHVybiBuZXcg Q29tbWFuZChhc3luYyAoY29udGFjdCkgPSZndDsgezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDC oMKgwqAgwqDCoMKgIGF3YWl0IENvcmVNZXRob2RzLlB1c2hQYWdlTW9kZWwmbHQ7Q29udGFjdFBh Z2VNb2RlbCZndDsoY29udGFjdCk7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIMKgwqDCoCB9KTs8 YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgfTxicj7CoMKgwqAgwqDCoMKgIH08YnI+PGJyPsKgwqDC oCDCoMKgwqAgLy8vICZsdDtzdW1tYXJ5Jmd0Ozxicj7CoMKgwqAgwqDCoMKgIC8vLyBSZXBvcHVs YXRlIHRoZSBjb2xsZWN0aW9uIHdpdGggdXBkYXRlZCBjb250YWN0cyBkYXRhLjxicj7CoMKgwqAg wqDCoMKgIC8vLyBOb3RlOiBGb3Igc2ltcGxpY2l0eSwgd2Ugd2FpdCBmb3IgdGhlIGFzeW5jIGRi IGNhbGwgdG8gY29tcGxldGUsPGJyPsKgwqDCoCDCoMKgwqAgLy8vIHJlY29tbWVuZCBtYWtpbmcg YmV0dGVyIHVzZSBvZiB0aGUgYXN5bmMgcG90ZW50aWFsLjxicj7CoMKgwqAgwqDCoMKgIC8vLyAm bHQ7L3N1bW1hcnkmZ3Q7PGJyPsKgwqDCoCDCoMKgwqAgcHJpdmF0ZSB2b2lkIExvYWRDb250YWN0 cygpPGJyPsKgwqDCoCDCoMKgwqAgezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBDb250YWN0cy5D bGVhcigpOzxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBUYXNrJmx0O0xpc3QmbHQ7Q29udGFjdCZn dDsmZ3Q7IGdldENvbnRhY3RzVGFzayA9IF9yZXBvc2l0b3J5LkdldEFsbENvbnRhY3RzKCk7PGJy PsKgwqDCoCDCoMKgwqAgwqDCoMKgIGdldENvbnRhY3RzVGFzay5XYWl0KCk7PGJyPsKgwqDCoCDC oMKgwqAgwqDCoMKgIGZvcmVhY2godmFyIGNvbnRhY3QgaW4gZ2V0Q29udGFjdHNUYXNrLlJlc3Vs dCkgezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDCoMKgwqAgQ29udGFjdHMuQWRkKGNvbnRhY3Qp Ozxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCB9PGJyPsKgwqDCoCDCoMKgwqAgfTxicj48YnI+wqDC oMKgIMKgwqDCoCAvLy8gJmx0O3N1bW1hcnkmZ3Q7PGJyPsKgwqDCoCDCoMKgwqAgLy8vIFVzZXMg dGhlIFNRTGl0ZSBBc3luYyBjYXBhYmlsaXR5IHRvIGluc2VydCBzYW1wbGUgZGF0YSBvbiBtdWx0 aXBsZSB0aHJlYWRzLjxicj7CoMKgwqAgwqDCoMKgIC8vLyAmbHQ7L3N1bW1hcnkmZ3Q7PGJyPsKg wqDCoCDCoMKgwqAgcHJpdmF0ZSB2b2lkIENyZWF0ZVNhbXBsZURhdGEoKTxicj7CoMKgwqAgwqDC oMKgIHs8YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgdmFyIGNvbnRhY3QxID0gbmV3IENvbnRhY3Qg ezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDCoMKgwqAgTmFtZSA9ICJKYWtlIFNtaXRoIiw8YnI+ wqDCoMKgIMKgwqDCoCDCoMKgwqAgwqDCoMKgIEVtYWlsID0gImpha2Uuc21pdGhAbWFpbG1haWwu Y29tIjxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCB9Ozxicj48YnI+PGJyPsKgwqDCoCDCoMKgwqAg wqDCoMKgIHZhciBjb250YWN0MiA9IG5ldyBDb250YWN0IHs8YnI+wqDCoMKgIMKgwqDCoCDCoMKg wqAgwqDCoMKgIE5hbWUgPSAiSmFuZSBTbWl0aCIsPGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIMKg wqDCoCBFbWFpbCA9ICJqYW5lLnNtaXRoQG1haWxtYWlsLmNvbSI8YnI+wqDCoMKgIMKgwqDCoCDC oMKgwqAgfTs8YnI+PGJyPjxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCB2YXIgY29udGFjdDMgPSBu ZXcgQ29udGFjdCB7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIMKgwqDCoCBOYW1lID0gIkppbSBC b2IiLDxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCDCoMKgwqAgRW1haWwgPSAiamltLmJvYkBtYWls bWFpbC5jb20iPGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIH07PGJyPjxicj7CoMKgwqAgwqDCoMKg IMKgwqDCoCB2YXIgdGFzazEgPSBfcmVwb3NpdG9yeS5DcmVhdGVDb250YWN0KGNvbnRhY3QxKTs8 YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgdmFyIHRhc2syID0gX3JlcG9zaXRvcnkuQ3JlYXRlQ29u dGFjdChjb250YWN0Mik7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIHZhciB0YXNrMyA9IF9yZXBv c2l0b3J5LkNyZWF0ZUNvbnRhY3QoY29udGFjdDMpOzxicj48YnI+wqDCoMKgIMKgwqDCoCDCoMKg wqAgLy8gRG9uJ3QgcHJvY2VlZCB1bnRpbCBhbGwgdGhlIGFzeW5jIGluc2VydHMgYXJlIGNvbXBs ZXRlLjxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCB2YXIgYWxsVGFza3MgPSBUYXNrLldoZW5BbGwo dGFzazEsIHRhc2syLCB0YXNrMyk7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIGFsbFRhc2tzLldh aXQoKTs8YnI+PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIExvYWRDb250YWN0cygpOzxicj7CoMKg wqAgwqDCoMKgIH08YnI+wqDCoMKgIH08YnI+fTxicj4KYGBg">​</div>
</div>
<br>
<font face="verdana,sans-serif">In the PCL, inside the Pages folder, create a
 new </font><font face="verdana,sans-serif"><font face="verdana,sans-serif">'Forms Xaml Page' </font>called ContactListPage with the following code (see code 
comments for descriptions):<br>
<br>
</font></div>
<div><font face="verdana,sans-serif"><b>ContactListPage.xaml</b><br>
</font></div>
<div><font face="verdana,sans-serif">
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(153,153,153);font-weight:bold">&lt;?xml version="1.0" encoding="utf-8" ?&gt;</span>
<span style="color:rgb(153,153,136);font-style:italic">&lt;!-- Add the xmlns:fresh line and use it to resolve the fresh:FreshBaseContentPage declaration --&gt;</span>
<span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">fresh:FreshBaseContentPage</span> <span style="color:rgb(0,128,128)">xmlns</span>=<span style="color:rgb(221,17,68)">"http://xamarin.com/schemas/2014/forms"</span>
                            <span style="color:rgb(0,128,128)">xmlns:x</span>=<span style="color:rgb(221,17,68)">"http://schemas.microsoft.com/winfx/2009/xaml"</span>
                            <span style="color:rgb(0,128,128)">x:Class</span>=<span style="color:rgb(221,17,68)">"FreshWithSQLite.Pages.ContactListPage"</span>
                            <span style="color:rgb(0,128,128)">xmlns:fresh</span>=<span style="color:rgb(221,17,68)">"clr-namespace:FreshMvvm;assembly=FreshWithSQLite"</span>&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">fresh:FreshBaseContentPage.ToolbarItems</span>&gt;</span>
    <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">ToolbarItem</span> <span style="color:rgb(0,128,128)">Text</span>=<span style="color:rgb(221,17,68)">"Add"</span> <span style="color:rgb(0,128,128)">Command</span>=<span style="color:rgb(221,17,68)">"{Binding AddContactCommand}"</span> /&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">fresh:FreshBaseContentPage.ToolbarItems</span>&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">ListView</span> <span style="color:rgb(0,128,128)">ItemsSource</span>=<span style="color:rgb(221,17,68)">"{Binding Contacts}"</span> <span style="color:rgb(0,128,128)">SelectedItem</span>=<span style="color:rgb(221,17,68)">"{Binding SelectedContact}"</span>&gt;</span>
    <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">ListView.ItemTemplate</span> &gt;</span>
      <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">DataTemplate</span>&gt;</span>
        <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">TextCell</span> <span style="color:rgb(0,128,128)">Text</span>=<span style="color:rgb(221,17,68)">"{Binding Name}"</span> <span style="color:rgb(0,128,128)">Detail</span>=<span style="color:rgb(221,17,68)">"{Binding Email}"</span> /&gt;</span>
      <span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">DataTemplate</span>&gt;</span>
    <span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">ListView.ItemTemplate</span>&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">ListView</span>&gt;</span>
<span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">fresh:FreshBaseContentPage</span>&gt;</span>
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgWE1MPGJyPiZsdDs/eG1sIHZlcnNpb249IjEuMCIgZW5jb2Rpbmc9InV0Zi04IiA/Jmd0Ozxi cj4mbHQ7IS0tIEFkZCB0aGUgeG1sbnM6ZnJlc2ggbGluZSBhbmQgdXNlIGl0IHRvIHJlc29sdmUg dGhlIGZyZXNoOkZyZXNoQmFzZUNvbnRlbnRQYWdlIGRlY2xhcmF0aW9uIC0tJmd0Ozxicj4mbHQ7 ZnJlc2g6RnJlc2hCYXNlQ29udGVudFBhZ2UgeG1sbnM9Imh0dHA6Ly94YW1hcmluLmNvbS9zY2hl bWFzLzIwMTQvZm9ybXMiPGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoCB4bWxuczp4PSJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dp bmZ4LzIwMDkveGFtbCI8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgIHg6Q2xhc3M9IkZyZXNoV2l0aFNRTGl0ZS5QYWdlcy5Db250YWN0TGlz dFBhZ2UiPGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoCB4bWxuczpmcmVzaD0iY2xyLW5hbWVzcGFjZTpGcmVzaE12dm07YXNzZW1ibHk9RnJl c2hXaXRoU1FMaXRlIiZndDs8YnI+wqAgJmx0O2ZyZXNoOkZyZXNoQmFzZUNvbnRlbnRQYWdlLlRv b2xiYXJJdGVtcyZndDs8YnI+wqDCoMKgICZsdDtUb29sYmFySXRlbSBUZXh0PSJBZGQiIENvbW1h bmQ9IntCaW5kaW5nIEFkZENvbnRhY3RDb21tYW5kfSIgLyZndDs8YnI+wqAgJmx0Oy9mcmVzaDpG cmVzaEJhc2VDb250ZW50UGFnZS5Ub29sYmFySXRlbXMmZ3Q7PGJyPsKgICZsdDtMaXN0VmlldyBJ dGVtc1NvdXJjZT0ie0JpbmRpbmcgQ29udGFjdHN9IiBTZWxlY3RlZEl0ZW09IntCaW5kaW5nIFNl bGVjdGVkQ29udGFjdH0iJmd0Ozxicj7CoMKgwqAgJmx0O0xpc3RWaWV3Lkl0ZW1UZW1wbGF0ZSAm Z3Q7PGJyPsKgwqDCoMKgwqAgJmx0O0RhdGFUZW1wbGF0ZSZndDs8YnI+wqDCoMKgwqDCoMKgwqAg Jmx0O1RleHRDZWxsIFRleHQ9IntCaW5kaW5nIE5hbWV9IiBEZXRhaWw9IntCaW5kaW5nIEVtYWls fSIgLyZndDs8YnI+wqDCoMKgwqDCoCAmbHQ7L0RhdGFUZW1wbGF0ZSZndDs8YnI+wqDCoMKgICZs dDsvTGlzdFZpZXcuSXRlbVRlbXBsYXRlJmd0Ozxicj7CoCAmbHQ7L0xpc3RWaWV3Jmd0Ozxicj4m bHQ7L2ZyZXNoOkZyZXNoQmFzZUNvbnRlbnRQYWdlJmd0Ozxicj4KYGBg">​</div>
</div>
<br>
<b>ContactListPage.xaml.cs</b></font><br>
<font face="verdana,sans-serif">
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshMvvm;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite.Pages
{
    <span style="color:rgb(153,153,136);font-style:italic">/// &lt;summary&gt;</span>
    <span style="color:rgb(153,153,136);font-style:italic">/// Update class to inherit from FreshBaseContentPage</span>
    <span style="color:rgb(153,153,136);font-style:italic">/// &lt;/summary&gt;</span>
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> partial <span style="color:rgb(51,51,51);font-weight:bold">class</span> ContactPage : FreshBaseContentPage
    {
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> ContactPage()
        {
            InitializeComponent();
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIEZyZXNoTXZ2bTs8YnI+PGJyPm5hbWVzcGFjZSBGcmVzaFdpdGhTUUxp dGUuUGFnZXM8YnI+ezxicj7CoMKgwqAgLy8vICZsdDtzdW1tYXJ5Jmd0Ozxicj7CoMKgwqAgLy8v IFVwZGF0ZSBjbGFzcyB0byBpbmhlcml0IGZyb20gRnJlc2hCYXNlQ29udGVudFBhZ2U8YnI+wqDC oMKgIC8vLyAmbHQ7L3N1bW1hcnkmZ3Q7PGJyPsKgwqDCoCBwdWJsaWMgcGFydGlhbCBjbGFzcyBD b250YWN0UGFnZSA6IEZyZXNoQmFzZUNvbnRlbnRQYWdlPGJyPsKgwqDCoCB7PGJyPsKgwqDCoCDC oMKgwqAgcHVibGljIENvbnRhY3RQYWdlKCk8YnI+wqDCoMKgIMKgwqDCoCB7PGJyPsKgwqDCoCDC oMKgwqAgwqDCoMKgIEluaXRpYWxpemVDb21wb25lbnQoKTs8YnI+wqDCoMKgIMKgwqDCoCB9PGJy PsKgwqDCoCB9PGJyPn08YnI+CmBgYA==">​</div>
</div>
</font>
<hr>
<h2>Setup initial navigation<br>
</h2>
<h3><a name="TOC-Overview"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif"><span style="font-family:verdana,sans-serif">Now we're nearly finished.&nbsp; We have created a Contact List page to display all of the user's contacts, and a Contact page where the user can add new contacts or edit existing contacts.&nbsp; The final step is to let the Forms application know we want to display our Contact List on startup.</span>&nbsp; To do this, we will edit the PCL's App.xaml.cs file to load the list page on application startup.<br>
</span></p>
<h3><a name="TOC-Create-a-FileAccessHelper-in-each-UI-project"></a><span style="font-family:verdana,sans-serif">Steps</span></h3>
<font face="verdana,sans-serif">Add the following code to the PCL's App.xaml.cs constructor, after InitializeComponent()<br>
</font></div>
<div><font face="verdana,sans-serif">
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%">var page = FreshPageModelResolver.ResolvePageModel&lt;ContactListPageModel&gt;();
var navContainer = <span style="color:rgb(51,51,51);font-weight:bold">new</span> FreshNavigationContainer(page);
MainPage = navContainer;
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnZhciBwYWdlID0gRnJlc2hQYWdlTW9kZWxSZXNvbHZlci5SZXNvbHZlUGFnZU1v ZGVsJmx0O0NvbnRhY3RMaXN0UGFnZU1vZGVsJmd0OygpOzxicj52YXIgbmF2Q29udGFpbmVyID0g bmV3IEZyZXNoTmF2aWdhdGlvbkNvbnRhaW5lcihwYWdlKTs8YnI+TWFpblBhZ2UgPSBuYXZDb250 YWluZXI7PGJyPgpgYGA=">​</div>
</div>
Your PCL's App.xaml.cs file should now look similar to this:</font><br>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshMvvm;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> FreshWithSQLite.PageModels;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Xamarin.Forms;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> FreshWithSQLite
{
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> partial <span style="color:rgb(51,51,51);font-weight:bold">class</span> App : Application
    {
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> App()
        {
            InitializeComponent();

            var page = FreshPageModelResolver.ResolvePageModel&lt;ContactListPageModel&gt;();
            var navContainer = <span style="color:rgb(51,51,51);font-weight:bold">new</span> FreshNavigationContainer(page);
            MainPage = navContainer;
        }

        <span style="color:rgb(51,51,51);font-weight:bold">protected</span> override <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnStart()
        {
            <span style="color:rgb(153,153,136);font-style:italic">// Handle when your app starts</span>
        }

        <span style="color:rgb(51,51,51);font-weight:bold">protected</span> override <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnSleep()
        {
            <span style="color:rgb(153,153,136);font-style:italic">// Handle when your app sleeps</span>
        }

        <span style="color:rgb(51,51,51);font-weight:bold">protected</span> override <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnResume()
        {
            <span style="color:rgb(153,153,136);font-style:italic">// Handle when your app resumes</span>
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIEZyZXNoTXZ2bTs8YnI+dXNpbmcgRnJlc2hXaXRoU1FMaXRlLlBhZ2VN b2RlbHM7PGJyPnVzaW5nIFhhbWFyaW4uRm9ybXM7PGJyPjxicj5uYW1lc3BhY2UgRnJlc2hXaXRo U1FMaXRlPGJyPns8YnI+wqDCoMKgIHB1YmxpYyBwYXJ0aWFsIGNsYXNzIEFwcCA6IEFwcGxpY2F0 aW9uPGJyPsKgwqDCoCB7PGJyPsKgwqDCoCDCoMKgwqAgcHVibGljIEFwcCgpPGJyPsKgwqDCoCDC oMKgwqAgezxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCBJbml0aWFsaXplQ29tcG9uZW50KCk7PGJy Pjxicj7CoMKgwqAgwqDCoMKgIMKgwqDCoCB2YXIgcGFnZSA9IEZyZXNoUGFnZU1vZGVsUmVzb2x2 ZXIuUmVzb2x2ZVBhZ2VNb2RlbCZsdDtDb250YWN0TGlzdFBhZ2VNb2RlbCZndDsoKTs8YnI+wqDC oMKgIMKgwqDCoCDCoMKgwqAgdmFyIG5hdkNvbnRhaW5lciA9IG5ldyBGcmVzaE5hdmlnYXRpb25D b250YWluZXIocGFnZSk7PGJyPsKgwqDCoCDCoMKgwqAgwqDCoMKgIE1haW5QYWdlID0gbmF2Q29u dGFpbmVyOzxicj7CoMKgwqAgwqDCoMKgIH08YnI+PGJyPsKgwqDCoCDCoMKgwqAgcHJvdGVjdGVk IG92ZXJyaWRlIHZvaWQgT25TdGFydCgpPGJyPsKgwqDCoCDCoMKgwqAgezxicj7CoMKgwqAgwqDC oMKgIMKgwqDCoCAvLyBIYW5kbGUgd2hlbiB5b3VyIGFwcCBzdGFydHM8YnI+wqDCoMKgIMKgwqDC oCB9PGJyPjxicj7CoMKgwqAgwqDCoMKgIHByb3RlY3RlZCBvdmVycmlkZSB2b2lkIE9uU2xlZXAo KTxicj7CoMKgwqAgwqDCoMKgIHs8YnI+wqDCoMKgIMKgwqDCoCDCoMKgwqAgLy8gSGFuZGxlIHdo ZW4geW91ciBhcHAgc2xlZXBzPGJyPsKgwqDCoCDCoMKgwqAgfTxicj48YnI+wqDCoMKgIMKgwqDC oCBwcm90ZWN0ZWQgb3ZlcnJpZGUgdm9pZCBPblJlc3VtZSgpPGJyPsKgwqDCoCDCoMKgwqAgezxi cj7CoMKgwqAgwqDCoMKgIMKgwqDCoCAvLyBIYW5kbGUgd2hlbiB5b3VyIGFwcCByZXN1bWVzPGJy PsKgwqDCoCDCoMKgwqAgfTxicj7CoMKgwqAgfTxicj59PGJyPgpgYGA=">​</div>
</div>
</div>
<div>
<hr>
<h2>Building and running<br>
</h2>
<h3><a name="TOC-Overview"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif"><span style="font-family:verdana,sans-serif">All the pieces are now in place, and it is time for us to run the application.&nbsp; Right-click the Droid, iOS, or UWP project </span>and select 'Set as StartUp Project'.&nbsp; Then run the program to see your creation in action!&nbsp; Below are screenshots of the application running on an Android simulator, an iPhone simulator, and a Windows Mobile simulator.<br>
</span></p>
<p><span style="font-family:verdana,sans-serif"><br>
</span></p>
<p><span style="font-family:verdana,sans-serif"><b>Android Simulator:</b></span></p>
<div style="display:block;text-align:left"><b><a href="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/13_DroidDone.png?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/13_DroidDone.png" style="width:100%" border="0"></a></b></div>
<b><br>
</b>
<p><span style="font-family:verdana,sans-serif"><b>iPhone Simulator:</b></span></p>
<div style="display:block;text-align:left"><b><a href="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/14_iPhoneDone.png?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/14_iPhoneDone.png" style="width:100%" border="0"></a></b></div>
<b><br>
</b>
<p><span style="font-family:verdana,sans-serif"><b>Windows Mobile Simulator:</b></span></p>
<div style="display:block;text-align:left"><b><a href="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/15_winDone.png?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/forms/freshmvvm-with-sqlite/15_winDone.png" style="width:100%" border="0"></a></b></div>
<b><br>
</b>
<hr>
<h2>Conclusion</h2>
<div></div>
<div><font face="verdana,sans-serif">After
 completing this tutorial, I hope you've gained a solid foundation 
in writing multi-platoform apps with Xamarin Forms, FreshMvvm, and SQLite.&nbsp; 
The tools you've worked with form a solid basis for developing 
production-grade applications.&nbsp; After mastering these core concepts, you
 should be well-equiped to move forward and start developing your own 
apps.<br>
<br>
</font></div>
<div><font face="verdana,sans-serif">If you are interested, you might try to see if you can add the feature to allow users to delete individual contacts, and you might try playing around with additional FreshMvvm navigation containers.<br>
</font></div>
<div><br>
<span style="font-family:verdana,sans-serif">I truly hope you found this tutorial to be both clear and informative, and please feel free to leave feedback.</span></div>
</div>
<div><font face="verdana,sans-serif"><br>
</font></div>
<div><font face="verdana,sans-serif">Again, the complete source code for this tutorial can be found on my GitHub page. <br>
</font><br>
<b>On GitHub: </b><font face="verdana,sans-serif"><span style="font-family:verdana,sans-serif"><a href="https://github.com/C0D3Name/FreshWithSQLite" target="_blank">https://github.com/C0D3Name/FreshWithSQLite</a></span><br>
<br>
</font></div>
<div></div>

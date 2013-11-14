<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/bootstrap.master" AutoEventWireup="false" CodeFile="infPanels_demo.aspx.vb" Inherits="sscr_infPanels_demo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="masterStylePlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<h1>How to use infPanels in the right column</h1>
<p>
<i>InfPanels</i> are classes in <i>App_Code/individual_code/infPanels</i> directory.
</p>
<p>
In your page's vb file override procedure <i>InitInfoPanels()</i> <b>to add infPanel or infPanelsSet that appears only on this site</b>. (as an example see the vb-code of the current site and infPanel_0004)
</p>
<p>
Create <i>infPanelSets</i> <b>to reuse a set of panels on multiple sites</b>. As an example see <i>App_Code/individual_code/infPanelsSets/infPanelsSet_example.vb</i>
</p>
<p>
<b>To display some infPanel or infPanelsSet on multiple pages do following</b>:
</p>
<ul>
<li>Create class in <i>App_Code/individual_code/contentFamilies</i> and inherit it from <i>GeneralWraperPage</i></li>
<li>in this class override the procedures <i>AddAllwaysObenPanels</i> and <i>AddAllwaysUntenPanels</i></li>
<li>inherit from the just created Class all pages you want to display this set of infPanels</li>
</ul>
<p>
As an example see class <i>App_Code/individual_code/contentFamilies/contentFamily_example.vb</i>. Note that this page (the page you are reading) inherits from <i>contentFamily_example</i>
</p>
<p><b>To add some infPanels to <i>ALL pages</i></b> add this infPanels to the <i>infPanelsSet_AllPagesOben.vb</i> or <i>infPanelsSet_AllPagesUnten.vb</i>. There no example for this case because this panel would appear on ALL pages of the presentation.</p>
<p><b>To force the page to NOT display some panel</b> override the <i>IndicateNotNeededPanels()</i> procedure on this page.<br />
As an example: you don't see the panel <i>infPanel_0003</i> even though it is added to the class <i>contentFamily_example</i>. See the vb-code of this page to see how I force the page to hide the infPanel_0003.
</p>
<p><b>* Note:</b> reduntant infPanels will be removed and therefore each infPanel will be schown only once.</p>

<h1>How to use RelatedLinksPanel</h1>
<p><b>* Note:</b>The <i>Related Links Panel</i> will be rendered <b>only</b> if there is at last one related link on this page.</p>
<p><b>* Note:</b> redundant links (links with the same url) will be automatically removed from related links panel</p>
<p><b>To display some Link or LinksSet on multiple pages</b> use the contentFamily-classes generated for infPanels</p>
<p>Related links are organised <b>similar to infPanels</b>.</p>
<p>The names of the procedures to override are similar to this of infPanels.</p>
<p>To get examples of organizing RelatedLinks see code of <i>this page</i>, see class <i>contentFamily_example</i>.</p>
</asp:Content>


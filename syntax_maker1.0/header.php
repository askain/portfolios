<?php
$pos = strrpos($_SERVER['REQUEST_URI'], "/syntax_maker");
?>
<nav class="navbar navbar-default">
  <div class="container-fluid">
    <!-- Brand and toggle get grouped for better mobile display -->
    <div class="navbar-header">
      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
        <span class="sr-only">Toggle navigation</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
      <a class="navbar-brand" target="_blank" href="http://getbootstrap.com/">Bootstrap</a>
	  <a class="navbar-brand" target="_blank" href="http://docs.oracle.com/cd/E11882_01/server.112/e25494/scheduse.htm">Oracle</a>
    </div>

    <!-- Collect the nav links, forms, and other content for toggling -->
    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
      <ul class="nav navbar-nav">
        <li <?php if ($pos !== false) { print "class='active'"; } ?>>
        	<a href="./syntax_maker.php">Oracle Job Syntax Maker <span class="sr-only">(current)</span></a>
        </li>
        <li <?php if ($pos === false) { print "class='active'"; } ?>>
        	<a href="./what_you_saved.php">What you saved <span class="badge" id="syntax_cnt_badge">0</span></a>
        </li>
      </ul>
      <ul class="nav navbar-nav navbar-right">
		<li><a href="./syntax_maker.php">New Syntax</a></li>
        <li><a href="#" onclick="javascript:if(confirm('Do you want to delete all?'))SyntaxRepository.clear();">Delete All Syntax</a></li>
      </ul>
    </div><!-- /.navbar-collapse -->
  </div><!-- /.container-fluid -->
</nav>

<script>
var hideJumboNotHtml5 = function()	{
	$("#jumbo_not_html5").hide("slow", function(){ $(this).remove(); });
};
var neverShowJumboNotHtml5 = function()	{
	hideJumboNotHtml5();
	$.cookie("neverShowJumboNotHtml5", "true", { expires: 365 });
};
$(function()	{
	
	SyntaxRepository.refreshBadge();
	
	if(typeof(Storage) === "undefined") {
		$("#save_cookie").removeClass("btn-success").addClass("disabled");
		
		var hide = $.cookie("neverShowJumboNotHtml5");
		if(hide == true)	{
			$("#jumbo_not_html5").remove();
		} else {
			$("#jumbo_not_html5").removeClass("hide");
		}
	}
});
</script>
	<div class="jumbotron hide" id="jumbo_not_html5">
		<h1>HTML5를 지원하는 브라우져가 아닙니다.</h1>
		<p>본 사이트의 기능을 모두 사용하기 위해서 HTML5를 지원하는 브라우져가 필요합니다.</p>
		<p>
			<a class="btn btn-primary btn-lg" href="#" role="button" onclick="hideJumboNotHtml5();">알았다</a>
			<a class="btn btn-danger btn-lg" href="#" role="button" onclick="neverShowJumboNotHtml5();">알았다니까</a>
		</p>
	</div>
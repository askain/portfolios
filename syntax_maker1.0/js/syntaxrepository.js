var SyntaxRepository = {
	/*init : function()	{
		SyntaxRepository.list = [];
		for(key in localStorage)	{
			if(key.indexOf(SyntaxRepository.prefix) === 0)
				SyntaxRepository.list.push(localStorage.getItem(key));
		}
	},*/
	prefix : "syntax.",
	count : function()	{
		var count = 0;
		for(key in localStorage)	{
			if(key.indexOf(SyntaxRepository.prefix) === 0)
				count += 1;
		}
		return count;
	},
	refreshBadge : function()	{		
		$("#syntax_cnt_badge").text(SyntaxRepository.count());
	},
	/*contain : function(syntax)	{
		if(SyntaxRepository.list.length === 0)	SyntaxRepository.init();
		
		for(key in localStorage)	{
			if(key === SyntaxRepository.prefix + syntax.job_name)	return true;
		}
		return false;
	},*/
	add : function(syntax)	{
		localStorage.setItem(SyntaxRepository.prefix + syntax.job_name, JSON.stringify(syntax));
		SyntaxRepository.refreshBadge();
	},
	get : function(job_name)	{
		return JSON.parse(localStorage.getItem(SyntaxRepository.prefix + job_name));
	},
	getAll : function()	{
		var list = [];
		for(key in localStorage)	{
			if(key.indexOf(SyntaxRepository.prefix) === 0)
				list.push(JSON.parse(localStorage.getItem(key)));
		}
		return list;
	},
	remove : function(job_name)	{
		localStorage.removeItem(SyntaxRepository.prefix + job_name);
	},
	clear : function()	{
		localStorage.clear();
		SyntaxRepository.refreshBadge();
	},
	setAllOld : function()	{
		var list = [];
		for(key in localStorage)	{
			if(key.indexOf(SyntaxRepository.prefix) === 0)
				var temp = JSON.parse(localStorage.getItem(key));
				temp.isNew = undefined;
				SyntaxRepository.add(temp);
		}
		return list;
	}
};
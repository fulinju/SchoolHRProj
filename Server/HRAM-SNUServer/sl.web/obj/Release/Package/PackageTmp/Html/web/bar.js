// JavaScript Document
function showtab(m,n,count){
	var strPic1;
	var strPic2;
	var tdcolor;
    if (m==1) {
	strPic1='#FFF';
	strPic2='#03a9f4';
		for(var i=1;i<=count;i++){
		if (i==n){			
			getObject('bg_'+m+'_'+i).style.background=strPic1;
			getObject('td_'+m+'_'+i).style.color='#000000';
			getObject('tab_'+m+'_'+i).style.display='';
			}
		else {
			getObject('bg_'+m+'_'+i).style.background=strPic2;
			getObject('td_'+m+'_'+i).style.color='#000000';
			getObject('tab_'+m+'_'+i).style.display='none';
			}
	}
}

	if (m==2 | m==3 | m==4 | m==5 | m==6 | m==7 | m==8 | m==9 | m==10) {
	strPic1='#03a9f4';
	strPic2='#FFF';
		for(var i=1;i<=count;i++){
		if (i==n){			
			getObject('bg_'+m+'_'+i).style.background=strPic1;
			getObject('td_'+m+'_'+i).style.color='#000000';
			getObject('tab_'+m+'_'+i).style.display='';
			}
		else {
			getObject('bg_'+m+'_'+i).style.background=strPic2;
			getObject('td_'+m+'_'+i).style.color='#000000';
			getObject('tab_'+m+'_'+i).style.display='none';
			}
	}
}
}
function getObject(objectId) {
    if(document.getElementById && document.getElementById(objectId)) {
	// W3C DOM
	return document.getElementById(objectId);
    } else if (document.all && document.all(objectId)) {
	// MSIE 4 DOM
	return document.all(objectId);
    } else if (document.layers && document.layers[objectId]) {
	// NN 4 DOM
	return document.layers[objectId];
    } else {
	return false;
    }
} // getObject
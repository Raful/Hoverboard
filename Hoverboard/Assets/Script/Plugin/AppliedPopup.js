private var showList = false;
private var listEntry = 0;
private var list : GUIContent[];
private var listStyle : GUIStyle;
private var picked = false;

 
   
function Start () {
	// Make some content for the popup list
	list = new GUIContent[5];
	list[0] = new GUIContent("Foo");
	list[1] = new GUIContent("Bar");
	list[2] = new GUIContent("Thing1");
	list[3] = new GUIContent("Thing2");
	list[4] = new GUIContent("Thing3");
 
	// Make a GUIStyle that has a solid white hover/onHover background to indicate highlighted items
	listStyle = new GUIStyle();
	listStyle.normal.textColor = Color.white;
	var tex = new Texture2D(2, 2);
	var colors = new Color[4];
	for (color in colors) color = Color.white;
	tex.SetPixels(colors);
	tex.Apply();
	listStyle.hover.background = tex;
	listStyle.onHover.background = tex;
	listStyle.padding.left = listStyle.padding.right = listStyle.padding.top = listStyle.padding.bottom = 4;
}
 
/*function OnGUI () {
	if (Popup.List(Rect(20,20,200,100),ref showList, ref listEntry, new GUIContent("hello"), list, listStyle)){
		picked = true;
	}
	if (picked) {
		GUI.Label (Rect(50, 70, 400, 20), "You picked " + list[listEntry].text + "!");
	}
}*/
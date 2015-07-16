using UnityEditor;


public class test : Editor {
	
	
	[MenuItem ("NavMesh/Build With Slope 120")] 
	
	public static void BuildSlope120 () { 
		SerializedObject obj = new SerializedObject (NavMeshBuilder.navMeshSettingsObject); 
		SerializedProperty prop = obj.FindProperty ("m_BuildSettings.agentSlope"); 
		prop.floatValue = 30.0f; 
	
		obj.ApplyModifiedProperties (); 
		NavMeshBuilder.BuildNavMesh (); 
	}
}
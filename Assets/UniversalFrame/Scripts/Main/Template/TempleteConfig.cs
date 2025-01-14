// Generated by github.com/davyxu/tabtoy
// Version: 2.8.10
// DO NOT EDIT!!
using System.Collections.Generic;

namespace Assets.Scripts
{
	
	

	// Defined in table: Config
	
	public partial class Config
	{
		/// <summary> 
		/// Role
		/// </summary>
		public List<RoleDefine> Role = new List<RoleDefine>(); 
		
		/// <summary> 
		/// Skin
		/// </summary>
		public List<SkinDefine> Skin = new List<SkinDefine>(); 
		
		/// <summary> 
		/// Animation
		/// </summary>
		public List<AnimationDefine> Animation = new List<AnimationDefine>(); 
	
	

	} 

	// Defined in table: Role
	[System.Serializable]
	public partial class RoleDefine
	{
	
		
		/// <summary> 
		/// 角色ID
		/// </summary>
		public int ID = 0; 
		
		/// <summary> 
		/// 角色名称
		/// </summary>
		public string Name = ""; 
		
		/// <summary> 
		/// 模型资源名称
		/// </summary>
		public string ModelResName = ""; 
		
		/// <summary> 
		/// 皮肤列表
		/// </summary>
		public List<int> Skins = new List<int>(); 
	
	

	} 

	// Defined in table: Skin
	[System.Serializable]
	public partial class SkinDefine
	{
	
		
		/// <summary> 
		/// ID
		/// </summary>
		public int ID = 0; 
		
		/// <summary> 
		/// 名称
		/// </summary>
		public string Name = ""; 
		
		/// <summary> 
		/// 描述
		/// </summary>
		public string Describtion = ""; 
		
		/// <summary> 
		/// 资源名称
		/// </summary>
		public string ResName = ""; 
	
	

	} 

	// Defined in table: Animation
	[System.Serializable]
	public partial class AnimationDefine
	{
	
		
		/// <summary> 
		/// ID
		/// </summary>
		public int ID = 0; 
		
		/// <summary> 
		/// 名称
		/// </summary>
		public string Name = ""; 
		
		/// <summary> 
		/// 眼睛动画标志
		/// </summary>
		public string EyeAnimationKey = ""; 
		
		/// <summary> 
		/// 是否有眼部动画
		/// </summary>
		public int HasWink = 0; 
		
		/// <summary> 
		/// 动画持续时间
		/// </summary>
		public int Duration = 0; 
	
	

	} 

}

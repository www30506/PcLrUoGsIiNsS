using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu( "UI/Effects/Gradient" )]
public class TextGradient : BaseMeshEffect
{
	[SerializeField]private Color32 topColor = Color.white;
	[SerializeField]private Color32 bottomColor = Color.black;
	private enum GradientType {Horizontal, Vertical};
	[SerializeField]private GradientType gradientType = GradientType.Horizontal;

	public override void ModifyMesh( VertexHelper vh )
	{
		float bottomY = -1;
		float topY = -1;

		for ( int i = 0; i < vh.currentVertCount; i++ )
		{
			UIVertex v = new UIVertex();
			vh.PopulateUIVertex( ref v, i );

			if(gradientType == GradientType.Horizontal){
				if ( bottomY == -1 ) 
					bottomY = v.position.x;
				if ( topY == -1 ) 
					topY = v.position.x;

				if ( v.position.x > topY ) 
					topY = v.position.x;
				else if ( v.position.x < bottomY ) 
					bottomY = v.position.x;
				
			}
			else if (gradientType == GradientType.Vertical){
				if ( bottomY == -1 ) 
					bottomY = v.position.y;
				if ( topY == -1 ) 
					topY = v.position.y;

				if ( v.position.y > topY ) 
					topY = v.position.y;
				else if ( v.position.y < bottomY ) 
					bottomY = v.position.y;
			}
		}


		float uiElementHeight = topY - bottomY;

		for ( int i = 0; i < vh.currentVertCount; i++ )
		{
			UIVertex v = new UIVertex();
			vh.PopulateUIVertex( ref v, i );
			if(gradientType == GradientType.Horizontal){
				v.color = Color32.Lerp( bottomColor, topColor, (v.position.x - bottomY) / uiElementHeight );
			}else if(gradientType == GradientType.Vertical){
				v.color = Color32.Lerp( bottomColor, topColor, (v.position.y - bottomY) / uiElementHeight );
			}
			vh.SetUIVertex( v, i );
		}
	}

}
using UnityEngine;
using System.Collections;

namespace ShaderForge {

	[System.Serializable]
	public class SFN_Final : SF_Node {




		public SF_NodeConnector
			 diffuse
			, specular
			, gloss
			, normal
			, emissive
			, alpha
			, alphaClip
			, diffusePower
			, refraction
			, transmission
			, lightWrap
			, ambientDiffuse
			, ambientSpecular
			, customLighting
			, outlineWidth
			, outlineColor
			//, anisotropicDirection
			, vertexOffset
			, displacement
			, tessellation
		;

		public SFN_Final() {

		}

		public SFN_Final Initialize( SF_Editor editor ) {
			base.Initialize( "Main" );
			this.editor = editor;
			base.showColor = true;
			this.id = ( editor.idIncrement++ );
			base.nodeName = "Main";
			Vector2 pos = new Vector2( 32768, 32768 );
			base.rect = new Rect( pos.x - NODE_WIDTH / 2, pos.y - NODE_HEIGHT / 2, NODE_WIDTH * 1.7f, 400 );


			this.connectors = new SF_NodeConnector[]{

		// SURFACE
				diffuse 				= SF_NodeConnector.Create(this,"diff",				   "Diffuse",	ConType.cInput, ValueType.VTvPending	,true,"float3(0,0,0)"	).Skip(PassType.ShadColl, PassType.ShadCast, PassType.Outline).TypecastTo(3),
				diffusePower 			= SF_NodeConnector.Create(this,"diffpow",		 "Diffuse Power",   ConType.cInput, ValueType.VTvPending	,true,"1"				).Skip(PassType.ShadColl, PassType.ShadCast, PassType.Outline),
				specular 				= SF_NodeConnector.Create(this,"spec",				  "Specular",	ConType.cInput, ValueType.VTvPending	,true					).Skip(PassType.ShadColl, PassType.ShadCast, PassType.Outline).TypecastTo(3),
				gloss 					= SF_NodeConnector.Create(this,"gloss",                 "Gloss",	ConType.cInput, ValueType.VTv1			,true,"0.5"				).Skip(PassType.ShadColl, PassType.ShadCast, PassType.Outline),
				normal 					= SF_NodeConnector.Create(this,"normal",			    "Normal",	ConType.cInput, ValueType.VTv3			,true					).Skip(PassType.ShadColl, PassType.ShadCast, PassType.Outline),
				emissive 				= SF_NodeConnector.Create(this,"emission",			  "Emission",	ConType.cInput, ValueType.VTvPending	,true,"float3(0,0,0)"	).Skip(PassType.ShadColl, PassType.ShadCast, PassType.Outline).TypecastTo(3),
				transmission 			= SF_NodeConnector.Create(this,"transm",		  "Transmission",	ConType.cInput, ValueType.VTvPending	,true					).Skip(PassType.ShadColl, PassType.ShadCast, PassType.Outline).TypecastTo(3),
				lightWrap 				= SF_NodeConnector.Create(this,"lwrap",		"Light Wrapping",   ConType.cInput, ValueType.VTvPending	,true					).Skip(PassType.ShadColl, PassType.ShadCast, PassType.Outline).TypecastTo(3),

		// LIGHTING
				ambientDiffuse 			= SF_NodeConnector.Create(this,"amdfl", "Diffuse Ambient Light",   ConType.cInput, ValueType.VTvPending	,true,"float3(0,0,0)"	).Skip(PassType.ShadColl, PassType.ShadCast, PassType.FwdAdd, PassType.Outline).TypecastTo(3),
				ambientSpecular 		= SF_NodeConnector.Create(this,"amspl","Specular Ambient Light",   ConType.cInput, ValueType.VTvPending	,true,"float3(0,0,0)"	).Skip(PassType.ShadColl, PassType.ShadCast, PassType.FwdAdd, PassType.Outline).TypecastTo(3),
				customLighting 			= SF_NodeConnector.Create(this,"custl",	   "Custom Lighting",   ConType.cInput, ValueType.VTvPending	,true					).Skip(PassType.ShadColl, PassType.ShadCast, PassType.Outline).TypecastTo(3),

		// TRANSPARENCY
				alpha 					= SF_NodeConnector.Create(this,"alpha",				 "Alpha",	ConType.cInput, ValueType.VTv1			,true,"1"				).Skip(PassType.ShadColl, PassType.ShadCast, PassType.Outline),
				alphaClip 				= SF_NodeConnector.Create(this,"clip",			    "Alpha Clip",	ConType.cInput, ValueType.VTv1			,true					),
				refraction 				= SF_NodeConnector.Create(this,"refract",		    "Refraction",	ConType.cInput, ValueType.VTv2			,true					).Skip(PassType.ShadColl, PassType.ShadCast, PassType.Outline).TypecastTo(2),

		// DEFORMERS
				outlineWidth 			= SF_NodeConnector.Create(this,"olwid",		 "Outline Width",   ConType.cInput, ValueType.VTv1			,true					).Skip(PassType.ShadColl, PassType.ShadCast, PassType.FwdAdd, PassType.FwdBase),
				outlineColor 			= SF_NodeConnector.Create(this,"olcol",		 "Outline Color",   ConType.cInput, ValueType.VTvPending	,true,"float3(0,0,0)"	).Skip(PassType.ShadColl, PassType.ShadCast, PassType.FwdAdd, PassType.FwdBase).TypecastTo(3),
				vertexOffset 			= SF_NodeConnector.Create(this,"voffset",		 "Vertex Offset",	ConType.cInput, ValueType.VTvPending	,true					).ForceBlock(ShaderProgram.Vert).TypecastTo(3),
				displacement 			= SF_NodeConnector.Create(this,"disp",		 "DX11 Displacement",	ConType.cInput, ValueType.VTv3			,true					).ForceBlock(ShaderProgram.Vert).TypecastTo(3),
				tessellation 			= SF_NodeConnector.Create(this,"tess",		 "DX11 Tessellation",	ConType.cInput, ValueType.VTv1			,true					).ForceBlock(ShaderProgram.Vert)
			};

			//distortion.enableState = EnableState.Disabled;
			//customLighting.enableState = EnableState.Disabled;
			//cusomLightingDiffuse.enableState = EnableState.Disabled;
			//anisotropicDirection.enableState = EnableState.Disabled;


			return this;

		}

		public override void OnUpdateNode( NodeUpdateType updType, bool cascade = true ) {
			if( cascade )
				editor.OnShaderModified( updType );
		}



	}
}
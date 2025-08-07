using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using IntraLattice.Properties;
using Rhino.Display;
using Rhino.Geometry;
using Rhino.Runtime;

namespace IntraLattice.CORE.UtilityModule;

public class MeshPreviewComponent : GH_Component
{
	private List<Mesh> m_mesh = new List<Mesh>();

	public override GH_Exposure Exposure => (GH_Exposure)8;

	protected override Bitmap Icon => Resources.meshPreview;

	public override Guid ComponentGuid => new Guid("{c5e3b143-5534-4ad3-a711-33881772d683}");

	public MeshPreviewComponent()
		: base("Mesh Preview", "MeshPreview", "Generates a preview of the mesh.", "IntraLattice", "Utils")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddMeshParameter("Mesh", "Mesh", "Mesh(es) to preview.", (GH_ParamAccess)1);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		List<Mesh> list = new List<Mesh>();
		if (DA.GetDataList<Mesh>(0, list) && list != null && list.Count != 0)
		{
			m_mesh = list;
		}
	}

	public override void DrawViewportMeshes(IGH_PreviewArgs args)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		DisplayMaterial val = new DisplayMaterial((Color)Color.FromArgb(255, 255, 255), 0.0);
		((GH_Component)this).DrawViewportMeshes(args);
		((GH_Component)this).DrawViewportWires(args);
		if (m_mesh == null)
		{
			return;
		}
		foreach (Mesh item in m_mesh)
		{
			if (item != null && ((CommonObject)item).IsValid)
			{
				args.Display.DrawMeshShaded(item, val);
				args.Display.DrawMeshWires(item, (Color)Color.Black);
			}
		}
	}
}

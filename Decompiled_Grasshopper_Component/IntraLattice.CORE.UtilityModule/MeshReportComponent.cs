using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using IntraLattice.Properties;
using Rhino.Geometry;
using Rhino.Runtime;

namespace IntraLattice.CORE.UtilityModule;

public class MeshReportComponent : GH_Component
{
	private Polyline[] m_nakedEdges;

	public override GH_Exposure Exposure => (GH_Exposure)8;

	protected override Bitmap Icon => Resources.meshReport;

	public override Guid ComponentGuid => new Guid("{f49535d8-ab4a-4ee7-8721-290457b4e3eb}");

	public MeshReportComponent()
		: base("Mesh Report", "MeshReport", "Verifies that the mesh represents a solid, and returns a comprehensive report.", "IntraLattice", "Utils")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddMeshParameter("Mesh", "Mesh", "Mesh to inspect.", (GH_ParamAccess)0);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddTextParameter("Report", "Report", "Report of inspection", (GH_ParamAccess)0);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		Mesh val = null;
		if (DA.GetData<Mesh>(0, ref val) && ((CommonObject)val).IsValid)
		{
			string text = "";
			bool flag = true;
			text = "- Details -\n";
			m_nakedEdges = val.GetNakedEdges();
			if (m_nakedEdges == null)
			{
				text += "Mesh has 0 naked edges. \n";
			}
			else
			{
				text += $"Mesh has {m_nakedEdges.Length} naked edges. \n";
				flag = false;
			}
			bool flag2 = default(bool);
			bool flag3 = default(bool);
			if (val.IsManifold(true, ref flag2, ref flag3))
			{
				text += "Mesh is manifold. \n";
			}
			else
			{
				text += "Mesh is non-manifold. \n";
				flag = false;
			}
			if (val.SolidOrientation() == 1)
			{
				text += "Mesh is solid. \n";
			}
			else if (val.SolidOrientation() == 0)
			{
				text += "Mesh is not solid. \n";
				flag = false;
			}
			else
			{
				val.Flip(true, true, true);
				text += "Mesh is solid. (normals have been flipped) \n";
			}
			text = ((!flag) ? ("Mesh is INVALID.\n\n" + text) : ("Mesh is VALID.\n\n" + text));
			text = "- Overview -\n" + text;
			DA.SetData(0, (object)text);
		}
	}

	public override void DrawViewportWires(IGH_PreviewArgs args)
	{
		((GH_Component)this).DrawViewportWires(args);
		if (m_nakedEdges == null)
		{
			return;
		}
		Polyline[] nakedEdges = m_nakedEdges;
		foreach (Polyline val in nakedEdges)
		{
			if (val.IsValid)
			{
				args.Display.DrawPolyline((IEnumerable<Point3d>)val, (Color)Color.DarkRed);
			}
		}
	}
}

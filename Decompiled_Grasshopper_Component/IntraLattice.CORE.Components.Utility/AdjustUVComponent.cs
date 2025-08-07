using System;
using System.Drawing;
using Grasshopper.Kernel;
using IntraLattice.Properties;
using Rhino.Geometry;

namespace IntraLattice.CORE.Components.Utility;

public class AdjustUVComponent : GH_Component
{
	protected override Bitmap Icon => Resources.adjustUV;

	public override Guid ComponentGuid => new Guid("{3372eac1-1545-4fca-9a25-72c4563aaa1f}");

	public AdjustUVComponent()
		: base("Adjust UV", "AdjustUV", "Adjusts the UV-map of a surface for proper alignment with other surfaces/axes.", "Intralattice", "Utils")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddSurfaceParameter("Surface", "Surf", "Surface to adjust.", (GH_ParamAccess)0);
		pManager.AddBooleanParameter("Swap UV", "SwapUV", "Swap the uv parameters.", (GH_ParamAccess)0, false);
		pManager.AddBooleanParameter("Reverse U", "ReverseU", "Reverse the u-parameter direction.", (GH_ParamAccess)0, false);
		pManager.AddBooleanParameter("Reverse V", "ReverseV", "Reverse the v-parameter direction.", (GH_ParamAccess)0, false);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddSurfaceParameter("Adjusted surface", "Surf", "Surface with adjusted uv-map.", (GH_ParamAccess)0);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		Surface val = null;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		if (DA.GetData<Surface>(0, ref val) && DA.GetData<bool>(1, ref flag) && DA.GetData<bool>(2, ref flag2) && DA.GetData<bool>(3, ref flag3) && val != null)
		{
			if (flag)
			{
				val = val.Transpose();
			}
			if (flag2)
			{
				val.Reverse(0, true);
			}
			if (flag3)
			{
				val.Reverse(1, true);
			}
			DA.SetData(0, (object)val);
		}
	}
}

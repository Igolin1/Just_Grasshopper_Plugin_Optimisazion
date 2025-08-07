using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;

namespace IntraLattice.CORE.Helpers;

public class InputTools
{
	public static void TopoSelect(ref IGH_Component Component, ref GH_Document GrasshopperDocument, int index, float offset)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		GH_ValueList val = new GH_ValueList();
		val.ListMode = (GH_ValueListMode)3;
		val.CreateAttributes();
		float x = ((IGH_DocumentObject)Component).Attributes.Pivot.X - 250f;
		float y = ((IGH_DocumentObject)Component).Attributes.Pivot.Y + (float)(index * 40) - offset;
		PointF pivot = new PointF(x, y);
		((GH_DocumentObject)val).Attributes.Pivot = pivot;
		val.ListItems.Clear();
		List<GH_ValueListItem> list = new List<GH_ValueListItem>();
		list.Add(new GH_ValueListItem("Grid", "0"));
		list.Add(new GH_ValueListItem("X", "1"));
		list.Add(new GH_ValueListItem("Star", "2"));
		list.Add(new GH_ValueListItem("Cross", "3"));
		list.Add(new GH_ValueListItem("Tesseract", "4"));
		list.Add(new GH_ValueListItem("Vintiles", "5"));
		list.Add(new GH_ValueListItem("Octet", "6"));
		list.Add(new GH_ValueListItem("Diamond", "7"));
		list.Add(new GH_ValueListItem("Honeycomb 1", "8"));
		list.Add(new GH_ValueListItem("Honeycomb 2", "9"));
		val.ListItems.AddRange(list);
		GrasshopperDocument.AddObject((IGH_DocumentObject)(object)val, false, int.MaxValue);
		Component.Params.Input[index].AddSource((IGH_Param)(object)val);
		((IGH_ActiveObject)Component.Params.Input[index]).CollectData();
	}

	public static void OrientSelect(ref IGH_Component Component, ref GH_Document GrasshopperDocument, int index, float offset)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		GH_ValueList val = new GH_ValueList();
		val.ListMode = (GH_ValueListMode)3;
		val.CreateAttributes();
		float x = ((IGH_DocumentObject)Component).Attributes.Pivot.X - 200f;
		float y = ((IGH_DocumentObject)Component).Attributes.Pivot.Y + (float)(index * 40) - offset;
		PointF pivot = new PointF(x, y);
		((GH_DocumentObject)val).Attributes.Pivot = pivot;
		val.ListItems.Clear();
		List<GH_ValueListItem> list = new List<GH_ValueListItem>();
		list.Add(new GH_ValueListItem("Default", "0"));
		list.Add(new GH_ValueListItem("RotateZ", "1"));
		list.Add(new GH_ValueListItem("RotateY", "2"));
		list.Add(new GH_ValueListItem("RotateX", "3"));
		val.ListItems.AddRange(list);
		GrasshopperDocument.AddObject((IGH_DocumentObject)(object)val, false, int.MaxValue);
		Component.Params.Input[index].AddSource((IGH_Param)(object)val);
		((IGH_ActiveObject)Component.Params.Input[index]).CollectData();
	}

	public static void GradientSelect(ref IGH_Component Component, ref GH_Document GrasshopperDocument, int index, float offset)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		GH_ValueList val = new GH_ValueList();
		val.ListMode = (GH_ValueListMode)1;
		val.CreateAttributes();
		float x = ((IGH_DocumentObject)Component).Attributes.Pivot.X - 200f;
		float y = ((IGH_DocumentObject)Component).Attributes.Pivot.Y + (float)(index * 40) - offset;
		PointF pivot = new PointF(x, y);
		((GH_DocumentObject)val).Attributes.Pivot = pivot;
		val.ListItems.Clear();
		List<GH_ValueListItem> list = new List<GH_ValueListItem>();
		list.Add(new GH_ValueListItem("Linear (X)", "0"));
		list.Add(new GH_ValueListItem("Linear (Y)", "1"));
		list.Add(new GH_ValueListItem("Linear (Z)", "2"));
		list.Add(new GH_ValueListItem("Centered (X)", "3"));
		list.Add(new GH_ValueListItem("Centered (Y)", "4"));
		list.Add(new GH_ValueListItem("Centered (Z)", "5"));
		list.Add(new GH_ValueListItem("Cylindrical (X)", "6"));
		list.Add(new GH_ValueListItem("Cylindrical (Y)", "7"));
		list.Add(new GH_ValueListItem("Cylindrical (Z)", "8"));
		list.Add(new GH_ValueListItem("Spherical", "9"));
		val.ListItems.AddRange(list);
		GrasshopperDocument.AddObject((IGH_DocumentObject)(object)val, false, int.MaxValue);
		Component.Params.Input[index].AddSource((IGH_Param)(object)val);
		((IGH_ActiveObject)Component.Params.Input[index]).CollectData();
	}
}

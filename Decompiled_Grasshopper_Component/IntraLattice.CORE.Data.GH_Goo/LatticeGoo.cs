using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Collections;
using Rhino.Geometry;

namespace IntraLattice.CORE.Data.GH_Goo;

public class LatticeGoo : GH_GeometricGoo<Lattice>, IGH_PreviewData
{
	public override bool IsValid
	{
		get
		{
			if (((GH_GeometricGoo<Lattice>)this).Value == null)
			{
				return false;
			}
			return base.IsValid;
		}
	}

	public override string IsValidWhyNot
	{
		get
		{
			if (((GH_GeometricGoo<Lattice>)this).Value.Nodes == null)
			{
				return "nodes empty";
			}
			if (((GH_GeometricGoo<Lattice>)this).Value.Struts == null)
			{
				return "struts empty";
			}
			return ((GH_Goo<Lattice>)(object)this).IsValidWhyNot;
		}
	}

	public override string TypeDescription => "Lattice Representation";

	public override string TypeName => "LatticeGoo";

	public override BoundingBox Boundingbox
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Expected O, but got Unknown
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			Point3dList val = new Point3dList();
			foreach (LatticeNode item in ((GH_GeometricGoo<Lattice>)this).Value.Nodes.AllData())
			{
				((RhinoList<Point3d>)(object)val).Add(item.Point3d);
			}
			return val.BoundingBox;
		}
	}

	public BoundingBox ClippingBox => ((GH_GeometricGoo<Lattice>)this).Boundingbox;

	public LatticeGoo()
	{
		((GH_GeometricGoo<Lattice>)this).Value = new Lattice();
	}

	public LatticeGoo(Lattice cell)
	{
		if (cell == null)
		{
			cell = new Lattice();
		}
		((GH_GeometricGoo<Lattice>)this).Value = cell;
	}

	public LatticeGoo DuplicateGoo()
	{
		return new LatticeGoo(((GH_GeometricGoo<Lattice>)this).Value.Duplicate());
	}

	public override IGH_GeometricGoo DuplicateGeometry()
	{
		return (IGH_GeometricGoo)(object)DuplicateGoo();
	}

	public override string ToString()
	{
		if (((GH_GeometricGoo<Lattice>)this).Value == null)
		{
			return "Null Lattice";
		}
		return ((GH_GeometricGoo<Lattice>)this).Value.ToString();
	}

	public override object ScriptVariable()
	{
		return ((GH_GeometricGoo<Lattice>)this).Value;
	}

	public override bool CastTo<Q>(out Q target)
	{
		if (typeof(Q).IsAssignableFrom(typeof(Lattice)))
		{
			if (((GH_GeometricGoo<Lattice>)this).Value == null)
			{
				target = default(Q);
			}
			else
			{
				target = (Q)(object)((GH_GeometricGoo<Lattice>)this).Value;
			}
			return true;
		}
		target = default(Q);
		return false;
	}

	public override bool CastFrom(object source)
	{
		if (source == null)
		{
			return false;
		}
		if (typeof(Lattice).IsAssignableFrom(source.GetType()))
		{
			((GH_GeometricGoo<Lattice>)this).Value = (Lattice)source;
			return true;
		}
		return false;
	}

	public override BoundingBox GetBoundingBox(Transform xform)
	{
		throw new NotImplementedException();
	}

	public override IGH_GeometricGoo Transform(Transform xform)
	{
		throw new NotImplementedException();
	}

	public override IGH_GeometricGoo Morph(SpaceMorph xmorph)
	{
		throw new NotImplementedException();
	}

	public void DrawViewportWires(GH_PreviewWireArgs args)
	{
	}

	public void DrawViewportMeshes(GH_PreviewMeshArgs args)
	{
	}
}

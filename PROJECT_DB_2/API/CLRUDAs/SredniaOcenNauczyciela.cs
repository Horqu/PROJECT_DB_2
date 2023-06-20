using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[SqlUserDefinedAggregate(
    Format.UserDefined,
    IsInvariantToNulls = true,
    IsInvariantToDuplicates = false,
    IsInvariantToOrder = false,
    MaxByteSize = -1)
]
public class SredniaOcenNauczyciela : IBinarySerialize
{
    private int liczbaOcen;
    private decimal sumaOcen;

    public void Init()
    {
        this.liczbaOcen = 0;
        this.sumaOcen = 0;
    }

    public void Accumulate(SqlDecimal ocena)
    {
        if (!ocena.IsNull)
        {
            this.liczbaOcen++;
            this.sumaOcen += ocena.Value;
        }
    }

    public void Merge(SredniaOcenNauczyciela other)
    {
        this.liczbaOcen += other.liczbaOcen;
        this.sumaOcen += other.sumaOcen;
    }

    public SqlDecimal Terminate()
    {
        if (this.liczbaOcen == 0)
            return SqlDecimal.Null;
        else
            return new SqlDecimal(this.sumaOcen / this.liczbaOcen);
    }

    public void Read(System.IO.BinaryReader r)
    {
        this.liczbaOcen = r.ReadInt32();
        this.sumaOcen = r.ReadDecimal();
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(this.liczbaOcen);
        w.Write(this.sumaOcen);
    }
}

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
public class LiczbaOcenPozNeg : IBinarySerialize
{
    private int ocenyMniejsze;
    private int ocenyWiekszeRowne;

    public void Init()
    {
        this.ocenyMniejsze = 0;
        this.ocenyWiekszeRowne = 0;
    }

    public void Accumulate(SqlDecimal ocena)
    {
        if (!ocena.IsNull)
        {
            if (ocena.Value < 3.0m)
                this.ocenyMniejsze++;
            else
                this.ocenyWiekszeRowne++;
        }
    }

    public void Merge(LiczbaOcenPozNeg other)
    {
        this.ocenyMniejsze += other.ocenyMniejsze;
        this.ocenyWiekszeRowne += other.ocenyWiekszeRowne;
    }

    public SqlString Terminate()
    {
        return $"Oceny < 3.0: {this.ocenyMniejsze} | Oceny >= 3.0: {this.ocenyWiekszeRowne}";
    }

    public void Read(System.IO.BinaryReader r)
    {
        this.ocenyMniejsze = r.ReadInt32();
        this.ocenyWiekszeRowne = r.ReadInt32();
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(this.ocenyMniejsze);
        w.Write(this.ocenyWiekszeRowne);
    }
}

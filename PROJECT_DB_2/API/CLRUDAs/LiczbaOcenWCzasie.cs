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
public class LiczbaOcen : IBinarySerialize
{
    private int liczbaOcen;

    public void Init()
    {
        this.liczbaOcen = 0;
    }

    public void Accumulate(SqlDecimal ocena, SqlDateTime data, SqlInt32 IdUcznia)
    {
        if (!ocena.IsNull && !data.IsNull)
        {
            this.liczbaOcen++;
        }
    }

    public void Merge(LiczbaOcen other)
    {
        this.liczbaOcen += other.liczbaOcen;
    }

    public SqlInt32 Terminate()
    {
        return new SqlInt32(this.liczbaOcen);
    }

    public void Read(System.IO.BinaryReader r)
    {
        this.liczbaOcen = r.ReadInt32();
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(this.liczbaOcen);
    }
}


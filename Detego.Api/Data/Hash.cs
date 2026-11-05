using System.ComponentModel.DataAnnotations.Schema;

namespace Detego.Api.Data;

[Table("safe_hashes")]
public class Hash
{
    [Column("hash_id")]
    public int Id { get; set; }

    [Column("sha1")]
    public string Code { get; set; } = string.Empty;
}

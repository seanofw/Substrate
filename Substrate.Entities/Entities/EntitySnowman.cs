using System;
using System.Collections.Generic;
using System.Text;

namespace Substrate.Entities
{
    using Substrate.Nbt;

    public class EntitySnowman : EntityMob
    {
        public static readonly SchemaNodeCompound SnowmanSchema = MobSchema.MergeInto(new SchemaNodeCompound("")
        {
            new SchemaNodeString("id", TypeId),
        });

        public static new string TypeId
        {
            get { return "SnowMan"; }
        }

        protected EntitySnowman (string id)
            : base(id)
        {
        }

        public EntitySnowman ()
            : this(TypeId)
        {
        }

        public EntitySnowman (TypedEntity e)
            : base(e)
        {
        }


        #region INBTObject<Entity> Members

        public override NbtErrors ValidateTree (TagNode tree)
        {
            return NbtVerifier.Verify(tree, SnowmanSchema);
        }

        #endregion


        #region ICopyable<Entity> Members

        public override TypedEntity Copy ()
        {
            return new EntitySnowman(this);
        }

        #endregion
    }
}

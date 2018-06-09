using System;
using System.Collections.Generic;
using System.Text;

namespace Substrate.Entities
{
    using Substrate.Nbt;

    public class EntityMonster : EntityMob
    {
        public static readonly SchemaNodeCompound MonsterSchema = MobSchema.MergeInto(new SchemaNodeCompound("")
        {
            new SchemaNodeString("id", TypeId),
        });

        public static new string TypeId
        {
            get { return "Monster"; }
        }

        protected EntityMonster (string id)
            : base(id)
        {
        }

        public EntityMonster ()
            : this(TypeId)
        {
        }

        public EntityMonster (TypedEntity e)
            : base(e)
        {
        }


        #region INBTObject<Entity> Members

        public override NbtErrors ValidateTree (TagNode tree)
        {
            return NbtVerifier.Verify(tree, MonsterSchema);
        }

        #endregion


        #region ICopyable<Entity> Members

        public override TypedEntity Copy ()
        {
            return new EntityMonster(this);
        }

        #endregion
    }
}

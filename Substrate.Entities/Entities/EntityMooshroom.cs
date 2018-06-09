using System;
using System.Collections.Generic;
using System.Text;

namespace Substrate.Entities
{
    using Substrate.Nbt;

    public class EntityMooshroom : EntityCow
    {
        public static readonly SchemaNodeCompound MooshroomSchema = CowSchema.MergeInto(new SchemaNodeCompound("")
        {
            new SchemaNodeString("id", TypeId),
        });

        public static new string TypeId
        {
            get { return "MushroomCow"; }
        }

        protected EntityMooshroom (string id)
            : base(id)
        {
        }

        public EntityMooshroom ()
            : this(TypeId)
        {
        }

        public EntityMooshroom (TypedEntity e)
            : base(e)
        {
        }


        #region INBTObject<Entity> Members

        public override NbtVerificationResults ValidateTree (TagNode tree)
        {
            return NbtVerifier.Verify(tree, MooshroomSchema);
        }

        #endregion


        #region ICopyable<Entity> Members

        public override TypedEntity Copy ()
        {
            return new EntityMooshroom(this);
        }

        #endregion
    }
}

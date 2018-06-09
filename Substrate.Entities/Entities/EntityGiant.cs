using System;
using System.Collections.Generic;
using System.Text;

namespace Substrate.Entities
{
    using Substrate.Nbt;

    public class EntityGiant : EntityMob
    {
        public static readonly SchemaNodeCompound GiantSchema = MobSchema.MergeInto(new SchemaNodeCompound("")
        {
            new SchemaNodeString("id", TypeId),
        });

        public static new string TypeId
        {
            get { return "Giant"; }
        }

        protected EntityGiant (string id)
            : base(id)
        {
        }

        public EntityGiant ()
            : this(TypeId)
        {
        }

        public EntityGiant (TypedEntity e)
            : base(e)
        {
        }


        #region INBTObject<Entity> Members

        public override NbtVerificationResults ValidateTree (TagNode tree)
        {
            return NbtVerifier.Verify(tree, GiantSchema);
        }

        #endregion


        #region ICopyable<Entity> Members

        public override TypedEntity Copy ()
        {
            return new EntityGiant(this);
        }

        #endregion
    }
}

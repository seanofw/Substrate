using System;
using System.Collections.Generic;
using System.Text;

namespace Substrate.Entities
{
    using Substrate.Nbt;

    public class EntityEnderPearl : EntityThrowable
    {
        public static readonly SchemaNodeCompound EnderPearlSchema = ThrowableSchema.MergeInto(new SchemaNodeCompound("")
        {
            new SchemaNodeString("id", TypeId),
        });

        public static string TypeId
        {
            get { return "ThrownEnderpearl"; }
        }

        protected EntityEnderPearl (string id)
            : base(id)
        {
        }

        public EntityEnderPearl ()
            : this(TypeId)
        {
        }

        public EntityEnderPearl (TypedEntity e)
            : base(e)
        {
        }


        #region INBTObject<Entity> Members

        public override NbtVerificationResults ValidateTree (TagNode tree)
        {
            return NbtVerifier.Verify(tree, EnderPearlSchema);
        }

        #endregion


        #region ICopyable<Entity> Members

        public override TypedEntity Copy ()
        {
            return new EntityEnderPearl(this);
        }

        #endregion
    }
}

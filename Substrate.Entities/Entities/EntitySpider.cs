﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Substrate.Entities
{
    using Substrate.Nbt;

    public class EntitySpider : EntityMob
    {
        public static readonly SchemaNodeCompound SpiderSchema = MobSchema.MergeInto(new SchemaNodeCompound("")
        {
            new SchemaNodeString("id", TypeId),
        });

        public static new string TypeId
        {
            get { return "Spider"; }
        }

        protected EntitySpider (string id)
            : base(id)
        {
        }

        public EntitySpider ()
            : this(TypeId)
        {
        }

        public EntitySpider (TypedEntity e)
            : base(e)
        {
        }


        #region INBTObject<Entity> Members

        public override NbtVerificationResults ValidateTree (TagNode tree)
        {
            return NbtVerifier.Verify(tree, SpiderSchema);
        }

        #endregion


        #region ICopyable<Entity> Members

        public override TypedEntity Copy ()
        {
            return new EntitySpider(this);
        }

        #endregion
    }
}

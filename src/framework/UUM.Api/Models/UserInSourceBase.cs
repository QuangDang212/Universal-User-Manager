﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Catel.Data;
using Catel.IoC;

using UUM.Api.Interfaces;

namespace UUM.Api.Models
{
    /// <summary>
    ///     Common properties for all users generated by plugins.
    ///     Each plugin may freely define additional attributes in its implementation.
    /// </summary>
    [Serializable]
    [KnownType("GetPluginTypes")]
    public abstract class UserInSourceBase : SavableModelBase<UserInSourceBase>, IIdentifiable
    {
        #region Constructors

        protected UserInSourceBase(Guid pluginId)
        {
            Id = Guid.NewGuid();
            PluginId = pluginId;
        }

        protected UserInSourceBase(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Properties

        #region Property: Id

        public Guid Id
        { get; set; }

        #endregion

        #endregion

        /// <summary>
        ///     The Guid of the plugin parameters that was used to create this user in source.
        ///     Identifies which plugin instance created this item. Used for serialization.
        /// </summary>
        public Guid PluginId
        { get; private set; }
	
        /// <summary>
        ///     The login for the target system, must be unique within the target system.
        ///     Used for associating local users from the remote users on update for example.
        /// </summary>
        //string LoginName { get; }
        
        /// <summary>
        /// Date of the last modification to this item
        /// </summary>
        public DateTime LastModificationDate { get; private set; }
        
        /// <summary>
        /// Flag telling if the last modification was manually checked
        /// </summary>
        public Boolean LastModificationAck { get; set; }
        
        //TODO: Keep history of modifications?
        
        public DateTime lastmodified {get; set; }
        
        #region KnownTypes
        static Type[] GetPluginTypes()
        {
            var types = new List<Type>();
			var pluginRepository = ServiceLocator.Default.ResolveType<IPluginRepository>();
            foreach (var plugin in pluginRepository.Plugins)
            {
                types.Add(plugin.GetUserInSourceType());
            }
            return types.ToArray();
        }
        #endregion
    }
}
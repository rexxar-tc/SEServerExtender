﻿using System;

using SEModAPIInternal.API.Common;
using SEModAPIInternal.Support;

namespace SEModAPIInternal.API.Entity
{
	public class PowerProducer
	{
		#region "Attributes"

		private PowerManager m_parent;
		private Object m_powerProducer;

		protected float m_maxPowerOutput;
		protected float m_powerOutput;

		public static string PowerProducerNamespace = "FB8C11741B7126BD9C97FE76747E087F";
		public static string PowerProducerClass = "7E69388ED0DB47818FB7AFF9F16C6EDA";

//		public static string PowerProducerGetMaxPowerOutputMethod = "7590FFDBE84D220BBAAFF91DA291E343";
//		public static string PowerProducerGetCurrentOutputMethod = "803904629C9D56CA2988EA0888B88985";
//		public static string PowerProducerSetCurrentOutputMethod = "5F242D4FA4534BA377325E7F75A2C34F";
		public static string PowerProducerGetMaxPowerOutputMethod = "FC47AEF2FFC2EE657980F000BB4CF9C8";
		public static string PowerProducerGetCurrentOutputMethod = "938A63711F788396FB324CC7F70A76C1";
		public static string PowerProducerSetCurrentOutputMethod = "A8AAD1094BC34280FD0B3367B9812915";

        #endregion

		#region "Constructors and Initializers"

		public PowerProducer(PowerManager parent, Object powerProducer)
		{
			m_parent = parent;
			m_powerProducer = powerProducer;

			m_maxPowerOutput = 0;
			m_powerOutput = 0;

			m_maxPowerOutput = MaxPowerOutput;
			m_powerOutput = PowerOutput;
		}

		#endregion

		#region "Properties"

		public float MaxPowerOutput
		{
			get
			{
				if (m_powerProducer == null)
					return m_maxPowerOutput;

				try
				{
					float result = (float)BaseObject.InvokeEntityMethod(m_powerProducer, PowerProducerGetMaxPowerOutputMethod);
					return result;
				}
				catch (Exception ex)
				{
					LogManager.ErrorLog.WriteLine(ex);
					return m_maxPowerOutput;
				}
			}
		}

		public float PowerOutput
		{
			get
			{
				if (m_powerProducer == null)
					return m_powerOutput;

				try
				{
					float result = (float)BaseObject.InvokeEntityMethod(m_powerProducer, PowerProducerGetCurrentOutputMethod);
					return result;
				}
				catch (Exception ex)
				{
					LogManager.ErrorLog.WriteLine(ex);
					return m_powerOutput;
				}
			}
			set
			{
				m_powerOutput = value;

				Action action = InternalUpdatePowerOutput;
				SandboxGameAssemblyWrapper.Instance.EnqueueMainGameAction(action);
			}
		}

		#endregion

		#region "Methods"

		public static bool ReflectionUnitTest()
		{
			try
			{
				Type type1 = SandboxGameAssemblyWrapper.Instance.GetAssemblyType(PowerProducerNamespace, PowerProducerClass);
				if (type1 == null)
					throw new Exception("Could not find internal type for PowerProducer");
				bool result = true;
				result &= BaseObject.HasMethod(type1, PowerProducerGetMaxPowerOutputMethod);
				result &= BaseObject.HasMethod(type1, PowerProducerGetCurrentOutputMethod);
				result &= BaseObject.HasMethod(type1, PowerProducerSetCurrentOutputMethod);

				return result;
			}
			catch (Exception ex)
			{
				LogManager.ErrorLog.WriteLine(ex);
				return false;
			}
		}

		protected void InternalUpdatePowerOutput()
		{
			BaseObject.InvokeEntityMethod(m_powerProducer, PowerProducerSetCurrentOutputMethod, new object[] { m_powerOutput });
		}

		#endregion
	}
}

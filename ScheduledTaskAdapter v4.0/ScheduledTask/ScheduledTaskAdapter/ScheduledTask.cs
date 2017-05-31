using System;
using Microsoft.BizTalk.Scheduler;

namespace ScheduledTaskAdapter
{
	/// <summary>
	///  ScheduledTask Class implementing the Microsoft.BizTalk.Scheduler.ITask interface.
	/// </summary>
	internal class ScheduledTask : ITask
	{
		// Events
		public event TaskProgressHandler Progress;
		public delegate void TaskDelegate();
		// Fields
		private string name;
		private ScheduledTask.TaskDelegate taskDelegate;
		// Properties
		public bool CanPause
		{
			get
			{
				return false;
			}
		}
		public bool CanStop
		{
			get
			{
				return true;
			}
		}
		public string Description
		{
			get
			{
				return "";
			}
		}
		public string Name
		{
			get
			{
				return this.name;
			}
		}
		// Methods
		public ScheduledTask(string name, ScheduledTask.TaskDelegate taskDelegate)
		{
			this.name = name;
			this.taskDelegate = taskDelegate;
		}

		private void FireProgress(TaskProgress progress)
		{
			if (this.Progress != null)
			{
				this.Progress(this, new TaskProgressEventArgs(progress));
			}
		}
		public void Pause()
		{
		}
		public void Resume()
		{
		}
		public void Start()
		{
			try
			{
				this.FireProgress(TaskProgress.Started);
				this.taskDelegate();
				this.FireProgress(TaskProgress.Succeeded);
			}
			catch (Exception)
			{
				this.FireProgress(TaskProgress.Failed);
			}
		}
		public void Stop()
		{
		}
	}
}

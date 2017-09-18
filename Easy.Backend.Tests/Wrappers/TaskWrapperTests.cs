using System;
using System.Threading;
using System.Threading.Tasks;
using Easy.Wrappers;
using NUnit.Framework;

namespace Easy.Handlers.Tests.Wrappers
{
	[TestFixture]
	public class TaskWrapperTests
	{
		private readonly TaskWrapper _wrapper = new TaskWrapper();
		
		[Test]
		public void StartTest()
		{
			var ev = new AutoResetEvent(false);
			var task = _wrapper.Start(() => ev.Set(), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Current);
			task.Wait();
			ev.WaitOne();
			Assert.IsTrue(true);			
		}
		
		[Test]
		public void ContinueWithTest()
		{
			var ev1 = new AutoResetEvent(false);
			var task = _wrapper.Start(() => ev1.Set(), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Current);
			var ev2 = new AutoResetEvent(false);
			var continuedTask = _wrapper.ContinueWith(task, (t, s)=>((AutoResetEvent)s).Set(), TaskContinuationOptions.None, ev2);
			continuedTask.Wait();
			ev1.WaitOne();
			ev2.WaitOne();
			Assert.IsTrue(true);			
		}
		
		[Test]
		public void DelayTest()
		{
			var now1 = DateTime.Now;
			_wrapper.Delay(TimeSpan.FromSeconds(5), CancellationToken.None);
			var now2 = DateTime.Now;
			var diff = now2 - now1;
			Assert.IsTrue(diff > TimeSpan.FromSeconds(4));
		}
		
		[Test]
		public void WaitAllTest()
		{
			var ev1 = new AutoResetEvent(false);
			var task1 = _wrapper.Start(() => ev1.WaitOne(), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Current);
			var ev2 = new AutoResetEvent(false);
			var task2 = _wrapper.Start(() => ev2.WaitOne(), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Current);
			var task3 = _wrapper.Start(() =>
			{
				_wrapper.Delay(TimeSpan.FromSeconds(3), CancellationToken.None);
				ev1.Set();
				ev2.Set();

			}, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Current);
			_wrapper.WaitAll(new []{task1, task2, task3});
			Assert.AreEqual(task1.Status, TaskStatus.RanToCompletion);
			Assert.AreEqual(task2.Status, TaskStatus.RanToCompletion);
			Assert.AreEqual(task3.Status, TaskStatus.RanToCompletion);
		}
		
		[Test]
		public void WaitAllTestWithTimeout()
		{
			var ev1 = new AutoResetEvent(false);
			var task1 = _wrapper.Start(() => ev1.WaitOne(), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Current);
			var ev2 = new AutoResetEvent(false);
			var task2 = _wrapper.Start(() => ev2.WaitOne(), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Current);
			var task3 = _wrapper.Start(() =>
			{
				_wrapper.Delay(TimeSpan.FromSeconds(3), CancellationToken.None);
				ev1.Set();
				ev2.Set();

			}, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Current);
			_wrapper.WaitAll(new []{task1, task2, task3}, TimeSpan.FromSeconds(7));
			Assert.AreEqual(task1.Status, TaskStatus.RanToCompletion);
			Assert.AreEqual(task2.Status, TaskStatus.RanToCompletion);
			Assert.AreEqual(task3.Status, TaskStatus.RanToCompletion);
		}
		
		[Test]
		public void WaitAllTestWithTimeoutFailed()
		{
			var ev1 = new AutoResetEvent(false);
			var task1 = _wrapper.Start(() => ev1.WaitOne(), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Current);
			var ev2 = new AutoResetEvent(false);
			var task2 = _wrapper.Start(() => ev2.WaitOne(), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Current);
			var task3 = _wrapper.Start(() =>
			{
				_wrapper.Delay(TimeSpan.FromSeconds(10), CancellationToken.None);
				ev1.Set();
				ev2.Set();

			}, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Current);
			Assert.Throws<TimeoutException>(() => _wrapper.WaitAll(new []{task1, task2, task3}, TimeSpan.FromSeconds(2)));
		}
		
	}
}
# AsyncDEMO_Net8
A cross-platform console application demonstrating async programming in C# and .NET 8.  It is loosely based on MS article "Asynchronous programming with async and await", https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/.

The various versions of the code, from fully synchronous to full asynchronous functionality, are saved side-by-side, rather than as different commits in source control.  The different versions can be accessed via a menu.

The scenario developed is cooking breakfast.  The tasks involved are:

1. Make coffee.
2. Heat a frying pan.
3. Fry two eggs.
4. Fry three slices of bacon.
5. Toast two pieces of bread.
6. Spread butter and jam on the toast.
7. Pour a glass of orange juice.

The synchronous version of the code executes the steps in the order shown:

![The steps in cooking breakfast the naive way, performing each task in turn synchronously](Images/SynchronousProcess.png "Synchronous process for cooking breakfast")

The time to cook breakfast is the sum of the times for each task.

Spreading the butter and jam on the toast and pouring the juice are quick tasks.  Making the coffee, heating the frying pan, frying the eggs, frying the bacon and making the toast are long-running tasks.  Heating the frying pan, frying the eggs and frying the bacon must be done sequentially, since there is only one frying pan.  The other long-running tasks can be done in parallel with the frying, however, to reduce the overall cooking time.  Ideally, we would want the various frying tasks, making the coffee and making the toast to complete at about the same time, so food or drink doesn't get cold.

So the most efficient way of executing the process would be:

![The steps in cooking breakfast the most efficient way, performing long-running tasks asynchronously](Images/FinalProcess.png "Most efficient process for cooking breakfast")

This requires the long-running steps to be performed asynchronously.

## Demo Application
The various tasks in the demo project have the following durations:

![A screenshot from the demo application, displaying the durations of each individual task](Images/IndividualTaskDurations.png "Individual task durations in demo application")

Note that in addition to the tasks required to make breakfast, the calling code has other work to perform as well.

### Synchronous Version
The synchronous version of the code executes each breakfast task sequentially, then it executes the other work performed by the calling code.  Since the synchronous code executes each task sequentially its duration is the sum of the durations of each individual task:

![A screenshot from the demo application, showing the tasks executed by the synchronous code, along with their durations](Images/Results_SyncVersion.png "Results of synchronous version")

### First Asynchronous Version
The first asynchronous version of the code calls each breakfast task with an immediate `await`.  As a result each breakfast task is still executed sequentially.  However, the `await`s are non-blocking, so execution can pass back to the calling code while the asynchronous breakfast tasks are executing.  This allows the calling code to complete its other work while the breakfast code is running.  As a result the execution time is just the sum of the durations of the breakfast tasks; the other work performed by the calling code executes at the same time:

![A screenshot from the demo application, showing the tasks executed by the asynchronous version of the code with immediate awaits](Images/Results_AsyncWithImmediateAwait.png "Results of asynchronous version with immediate awaits")

### Asynchronous Version with Deferred awaits
The second asynchronous version of the code defers the `await`s for each asynchronous breakfast task.  As a result the asynchronous tasks execute consecutively, not sequentially, so this version is very fast.  However, it has two problems:

1. The various tasks relating to frying execute consecutively.  So it attempts to fry the eggs before the pan has finished heating, and fry the bacon before the eggs have finished.

1. The tasks for spreading the butter and jam on the toast have to wait for the bacon to finish frying, the coffee to finish brewing, and the toast to finish.  This may result in an unnecessary delay, as spreading the butter and jam should only depend on the toast being ready; it's okay for those tasks to execute while the bacon is still frying and the coffee is still brewing.

![A screenshot from the demo application, showing the tasks executed by the asynchronous version of the code with deferred awaits](Images/Results_AsyncWithDeferredAwait.png "Results of asynchronous version with deferred awaits")

### Asynchronous Version with Deferred awaits and Task Composition
The third asynchronous version of the code adds task composition to fix the second problem: Allowing the spreading of the butter and jam to occur even if the bacon is still frying and the coffee is still brewing.  However, it still has the first problem where the various frying-related tasks execute consecutively:

![A screenshot from the demo application, showing the tasks executed by the asynchronous version of the code with deferred awaits and task composition](Images/Results_AsyncWithDeferredAwaitAndTaskComposition.png "Results of asynchronous version with deferred awaits and task composition")

### Final Asynchronous Version
The fourth and final asynchronous version of the code uses a combination of immediate and deferred `await`s to fix the first problem, where the various frying-related tasks were executing consecutively.  As a result it runs slower than the purely deferred `await` version of the code, but it executes the tasks in the correct order:

![A screenshot from the demo application, showing the tasks executed by the final asynchronous version of the code](Images/Results_AsyncFinal.png "Results of the final asynchronous version")


// This file is used to tell the compiler to override `async void` methods using ProtoPromise instead of Tasks.

#pragma warning disable CS0436 // Type conflicts with imported type

using Cysharp.Threading.Tasks.CompilerServices;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Provides a builder for asynchronous methods that return void, using the Promise system instead of the Task system.
    /// This type is intended for compiler use only.
    /// </summary>
    internal struct AsyncVoidMethodBuilder
    {
        // This must not be readonly.
        private AsyncUniTaskVoidMethodBuilder _target;

        private AsyncVoidMethodBuilder(AsyncUniTaskVoidMethodBuilder target)
        {
            _target = target;
        }

        /// <summary>Initializes a new <see cref="AsyncVoidMethodBuilder"/>.</summary>
        /// <returns>The initialized <see cref="AsyncVoidMethodBuilder"/>.</returns>
        public static AsyncVoidMethodBuilder Create()
        {
            return new AsyncVoidMethodBuilder(AsyncUniTaskVoidMethodBuilder.Create());
        }

        /// <summary>Initiates the builder's execution with the associated state machine.</summary>
        /// <typeparam name="TStateMachine">Specifies the type of the state machine.</typeparam>
        /// <param name="stateMachine">The state machine instance, passed by reference.</param>
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            _target.Start(ref stateMachine);
            _target.Task.Forget();
        }

        /// <summary>Does nothing.</summary>
        /// <param name="stateMachine">The heap-allocated state machine object.</param>
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            _target.SetStateMachine(stateMachine);
        }

        /// <summary>
        /// Schedules the specified state machine to be pushed forward when the specified awaiter completes.
        /// </summary>
        /// <typeparam name="TAwaiter">Specifies the type of the awaiter.</typeparam>
        /// <typeparam name="TStateMachine">Specifies the type of the state machine.</typeparam>
        /// <param name="awaiter">The awaiter.</param>
        /// <param name="stateMachine">The state machine.</param>
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            _target.AwaitOnCompleted(ref awaiter, ref stateMachine);
        }

        /// <summary>
        /// Schedules the specified state machine to be pushed forward when the specified awaiter completes.
        /// </summary>
        /// <typeparam name="TAwaiter">Specifies the type of the awaiter.</typeparam>
        /// <typeparam name="TStateMachine">Specifies the type of the state machine.</typeparam>
        /// <param name="awaiter">The awaiter.</param>
        /// <param name="stateMachine">The state machine.</param>
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            _target.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
        }

        /// <summary>
        /// Completes the method builder successfully.
        /// </summary>
        public void SetResult()
        {
            _target.SetResult();
        }

        /// <summary>
        /// Faults the method builder with an exception.
        /// </summary>
        /// <param name="exception">The exception that is the cause of this fault.</param>
        public void SetException(Exception exception)
        {
            _target.SetException(exception);
        }
    }
}
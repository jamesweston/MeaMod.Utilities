using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace MeaMod.Utilities
{
    /// <summary>
	/// async ServiceController.WaitForStatus class
	/// </summary>
	/// <see cref="https://stackoverflow.com/questions/38236238"/>
    public static class ServiceControllerExtensions
    {
        public async static Task WaitForStatusAsync(this ServiceController controller, ServiceControllerStatus desiredStatus, TimeSpan timeout)
        {
            var utcNow = DateTime.UtcNow;
            controller.Refresh();
            while (controller.Status != desiredStatus)
            {
                if (DateTime.UtcNow - utcNow > timeout)
                {
                    throw new System.ServiceProcess.TimeoutException($"Failed to wait for '{controller.ServiceName}' to change status to '{desiredStatus}'.");
                }

                await Task.Delay(250).ConfigureAwait(false);
                controller.Refresh();
            }
        }

        public static async Task WaitForStatusAsync(this ServiceController controller, ServiceControllerStatus desiredStatus, TimeSpan timeout, CancellationToken cancellationToken)
        {
            var utcNow = DateTime.UtcNow;
            controller.Refresh();
            while (controller.Status != desiredStatus)
            {
                if (DateTime.UtcNow - utcNow > timeout)
                {
                    throw new System.ServiceProcess.TimeoutException($"Failed to wait for '{controller.ServiceName}' to change status to '{desiredStatus}'.");
                }
                await Task.Delay(250, cancellationToken)
                    .ConfigureAwait(false);
                controller.Refresh();
            }
        }

    }

}
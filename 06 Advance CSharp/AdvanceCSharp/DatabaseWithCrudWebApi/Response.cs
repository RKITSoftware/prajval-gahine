using System.Net;
/// <summary>
/// Represents a response information object containing success status, message, and data.
/// </summary>
public class Response
{
    #region Public Properties

    /// <summary>
    /// Gets or sets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsError { get; set; }

    /// <summary>
    /// Gets or sets status code associated with the response.
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; set; }

    /// <summary>
    /// Gets or sets the message associated with the response.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the data associated with the response.
    /// </summary>
    public object Data { get; set; }

    #endregion
}
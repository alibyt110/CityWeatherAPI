1 - How much time did you spend on this task? If you had more time, what improvements or additions would you make?
 It took me about two days to complete the task.

2-What is the most useful feature recently added to your favorite programming language?
Please include a code snippet to demonstrate how you use it.
Primary Constructor and File-Scoped Namespace features:
As an example :
namespace Authenticate.Controllers;
public class AuthenticateController(ILogger<WeatherController> logger) : ControllerBase
{
    private readonly ILogger<WeatherController> _logger = logger;
}

3-How do you identify and diagnose a performance issue in a production environment? Have you done this before?
was using a third-party service to retrieve customer data when I noticed that as the number of customers increased, the response time from the service became longer. I suspected that the issue might be related to the internet connection. However, after conducting further investigation and reviewing the logs, I discovered that there was a bottleneck in the third-party service where customer data was being retrieved. After discussing this with a technical expert, we requested an alternative service to resolve the issue.

4-What’s the last technical book you read or technical conference you attended?
What did you learn from it?
I attended a webinar about the Event-Driven Pattern, where the differences between Event-Driven and Message-Driven architectures were discussed.
It covered how we can avoid data loss and what patterns we can use to achieve that.
One of the patterns explained was the Chain of Persistence Pattern, also referred to as the Forward Event Pattern.
The idea is that at each stage of the process, we persist the data to ensure reliability. For example, when an event is received, we persist it first — so that if the message queue fails, we still have the data and can reload it.
Only after persisting the data do we commit it to the database. Then we notify the queue that the data has been successfully stored and it can be safely removed from the queue.
This pattern helps with data integrity, but it also comes with a cost — it affects performance, and there is also a possibility of creating duplicate entries.
In an event-driven system, the contract is usually defined by the sender — meaning the sender determines the structure and format of the data that is pushed to the queue

5- What’s your opinion about this technical test?
It was very good and gave me the opportunity to review my knowledge and learn some new things. Thank you!

6-Please describe yourself using JSON format.
{
  "name": "Ali",
  "profession": "Backend Developer",
  "skills": ["C#", ".NET", "SQL", "Clean Architecture","Clean Code"],
  "experience": "6 years",
  "interests": ["Coding", "Learning new technologies"],
  "location": "Tehran, Iran"
}



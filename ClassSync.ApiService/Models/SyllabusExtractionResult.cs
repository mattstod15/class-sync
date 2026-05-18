namespace ClassSync.ApiService.Models;

public record SyllabusExtractionResult(
    string CourseCode,
    string CourseName,
    List<CalendarEvent> CalendarEvents,
    List<ReadingTask> ReadingTasks,
    List<CoursePolicy> Policies
);

public record CalendarEvent(
    string Title,
    DateTime? CalculatedDate, // Populated deterministically by backend
    string RawDateString,     // Extracted verbatim from syllabus text
    string EventType,         // e.g., "Exam", "Assignment", "Project"
    string Weight             // e.g., "15%"
);

public record ReadingTask(
    int WeekNumber,
    string Topic,
    string AssignedReadings
);

public record CoursePolicy(
    string Category,          // e.g., "Attendance", "Late Work", "AI Tooling"
    string Summary
);
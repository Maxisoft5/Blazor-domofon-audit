import { ApplicationStatuses } from "./application-statuses";

export class ApplicationStatusesConstants {

    public static StatusesDict =
    [
        {
            status: "notAssignedColumn", value: ApplicationStatuses.ReadyForAssign
        },
        {
            status: "assignedColumn", value: ApplicationStatuses.Assigned
        },
        {
            status: "completedColumn", value: ApplicationStatuses.Resolved
        },
        {
            status: "closedColumn", value: ApplicationStatuses.Rejected
        }
    ];

}
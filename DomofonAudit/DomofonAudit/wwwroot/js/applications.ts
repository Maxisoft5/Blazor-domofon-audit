import * as dragula from "dragula";
import * as $ from "jquery";
import { ApplicationStatusesConstants } from "./shared/applications-status-constants";


class Helpers {
    static dotNetHelper:any;

    static setDotNetHelper(value:any) {
        Helpers.dotNetHelper = value;
    }

    static async changeLoadingDashboard(value:any) {
        await Helpers.dotNetHelper.invokeMethodAsync("ChangeLoadingDashboard", value);
    }
}

declare global {
    interface Window {
        Helpers: Helpers;
    }
}

window.Helpers = Helpers;


function subscribeAllTicketsDragging() {

    dragula(
        {
            isContainer: function (el) {
                return el.classList.contains('notAssignedColumn') || el.classList.contains('assignedColumn') || 
                    el.classList.contains('completedColumn') || el.classList.contains('closedColumn');
            }
        }
    )
        .on("drag", function (e) {

        })
        .on("drop", function (el) {
            let statusColumnName: string = el.parentElement.classList[0];
            let column = ApplicationStatusesConstants.StatusesDict.find(x => x.status == statusColumnName);
            let applications = Array.from(el.parentElement.children);
            let droppedIndex = applications.indexOf(el);
            let id = $(el).find(".hided");
            let idValue = $(id).text();
            const formData = new FormData();
            formData.append("Status", column.value.toString());
            formData.append("Index", droppedIndex.toString());
            formData.append("ApplicationId", idValue);
            Helpers.changeLoadingDashboard(true);
            $.ajax({
                url: "api/Applications/MoveInDashboard",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (e) {
                    Helpers.changeLoadingDashboard(false);
                },
                error: function (err) {
                    console.log(err);
                    Helpers.changeLoadingDashboard(false);
                }
            });

        })
        .on("over", function (el, container) {
        })
        .on("out", function (el, container) {
        });
}

subscribeAllTicketsDragging();

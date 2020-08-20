import { UploadReportItem } from "./upload-report-item.model";

export class UploadReport {
    success: boolean;
    error: string;
    items: UploadReportItem[]
}
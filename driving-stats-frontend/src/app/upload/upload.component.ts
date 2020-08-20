import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { HttpEventType, HttpResponse } from '@angular/common/http'
import { FileUploadService } from "../services/file-upload.service";
import { UploadReport } from "./upload-report.model";

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  @ViewChild("result") resultDiv: ElementRef;
  uploadReport: UploadReport = null;
  apiErrorMessage: string = null;
  waitingForResponse: boolean = false;

  constructor(private fileUploadService: FileUploadService) { }

  ngOnInit(): void { }

  selectFile(event) {
    this.uploadFile(event.target.files);
  }

  onDragOverFile(event) {
    event.stopPropagation();
    event.preventDefault();
  }

  onDropFile(event: DragEvent) {
    event.preventDefault();
    this.uploadFile(event.dataTransfer.files);
  }

  private uploadFile(files: FileList) {
    if (files.length == 0) {
      console.log("No file selected!");
      return
    }

    let file: File = files[0];
    this.waitingForResponse = true;
    this.fileUploadService.uploadFile(file)
      .subscribe(
        uploadReport => {
          this.uploadReport = uploadReport;
          this.apiErrorMessage = null;
        },
        err => {
          this.apiErrorMessage = JSON.stringify(err)
          this.waitingForResponse = false
        },
        () => this.waitingForResponse = false
      )
  }

  private onUploadFileResponse(event: HttpResponse<any>) {
    this.uploadReport = new UploadReport();
    Object.assign(this.uploadReport, event.body);
    console.log(this.uploadReport);
  }

}

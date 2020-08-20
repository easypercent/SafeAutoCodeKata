import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpRequest, HttpEvent, HttpEventType, HttpResponse } from '@angular/common/http';
import { Observable, pipe, of, throwError } from "rxjs";
import { UploadReport } from '../upload/upload-report.model';
import { map, catchError } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {

  constructor(private http: HttpClient) { }

  uploadFile(file: File): Observable<UploadReport> {
    let url = "https://localhost:44315/api/upload";

    let formData = new FormData();
    formData.append('fileSelect', file);

    let params = new HttpParams();

    const options = {
      params: params,
      reportProgress: true,
    };

    const req = new HttpRequest('POST', url, formData, options);
    return this.http.request(req).pipe(
      map(
        event => {
          if (event instanceof HttpResponse) {
            var uploadReport = new UploadReport();
            Object.assign(uploadReport, event.body);
            return uploadReport;
          }
        }
      ), catchError(err => throwError(err))
    );
  }
}

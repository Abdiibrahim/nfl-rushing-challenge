import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { ReturnData, RushingItem } from 'src/app/_interfaces/rushing-item';
import { RushingService } from 'src/app/_services/rushing.service';
import { NgbdSortableHeader, SortEvent } from '../../_directives/ngbd-sortable-header.directive';

/**
 * Rushing table component
 */
@Component({
  selector: 'app-rushing-table',
  templateUrl: './rushing-table.component.html',
  styleUrls: ['./rushing-table.component.css']
})
export class RushingTableComponent implements OnInit {

  /**
   * sortable table header
   */
  @ViewChildren(NgbdSortableHeader) headers!: QueryList<NgbdSortableHeader>;

  public returnData!: ReturnData;
  public items!: RushingItem[];

  public page = 1;
  public pageSize = 10;
  public collectionSize = 0;
  public sortColumn = '';
  public sortDirection = '';
  public orderBy = '';
  public playerFilter = '';

  /**
   * Creates instance of component
   * @param _rushingService
   */
  constructor(private _rushingService: RushingService) {
  }

  /**
   * On component init
   */
  ngOnInit(): void {
    this.getData(this.page, this.pageSize, this.orderBy, this.playerFilter);
  }

  /**
   * Gets table data
   * @param pageNum 
   * @param pageSize 
   * @param orderBy 
   * @param filter 
   */
  public getData(pageNum: number, pageSize: number, orderBy: string, filter: string) {
    this._rushingService.getData(pageNum, pageSize, orderBy, filter).subscribe(response => {
      this.returnData = <ReturnData>(response.body);
      this.collectionSize = this.returnData.totalCount;
      // map row number onto items
      this.items = (<ReturnData>response.body).data.map((item, i) => ({id: (this.pageSize * (this.page - 1)) + i + 1, ...item}));
    },
    error => console.error(error));
  }
  
  /**
   * uses custom sort event directive to set column and sort direction
   * @param {column, direction}
   */
  public onSort({column, direction}: SortEvent): void {
    // resetting other headers
    this.headers.forEach(header => {
      if (header.sortable !== column){
        header.direction = '';
      }
    });

    if (direction === '' || column === ''){
      this.sortColumn = '';
      this.sortDirection = '';
      this.orderBy = '';
    } else {
      this.sortColumn = column;
      this.sortDirection = direction;
      this.orderBy = this.sortColumn + " " + this.sortDirection;
    }

    this.getData(this.page, this.pageSize, this.orderBy, this.playerFilter);
  }

  /**
   * Downloads csv from api
   * @param orderBy
   * @param filter
   */
  public downloadCsv(orderBy: string, filter: string): void {
    this._rushingService.exportToCsv(orderBy, filter).subscribe((event) => {
      if (event.type === HttpEventType.Response) {
        this._downloadFile(event);
      }
    });
  }

  /**
   * Opens file save dialog and sets default filename
   * @param data
   */
  private _downloadFile(data: HttpResponse<Blob>): void {
    const downloadFile = new Blob(<BlobPart[]>[data.body], { type: (<Blob>data.body).type });
    const a = document.createElement('a');
    a.setAttribute('style', 'display: none');
    document.body.appendChild(a);
    a.download = "rushing.csv";
    a.href = URL.createObjectURL(downloadFile);
    a.target = '_blank';
    a.click();
    document.body.removeChild(a);
  }

}

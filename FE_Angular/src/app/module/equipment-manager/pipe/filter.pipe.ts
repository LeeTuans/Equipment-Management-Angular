import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterPipe'
})
export class FilterPipe implements PipeTransform {

  transform<T>(values: T[], paginationData: any, filterValue: string, ...args: any[]): T[] {
    let listFilter: T[] = [];
    if (!filterValue || filterValue.length === 0) {
      listFilter = values;
    } else if (values.length === 0) {
      listFilter = values;
    } else {
      listFilter = values.filter((value: any) => {
        let found!: boolean;
        if (args.length === 0) {
          for (const key in value) {
            found = value[key].toString().toLowerCase().indexOf(filterValue.toLowerCase()) !== -1;
            if (found) break;
          }
        } else {
          for (let i = 0; i < args.length; i++) {
            found = value[args[i]].toLowerCase().indexOf(filterValue.toLowerCase()) !== -1;
            if (found) break;
          }
        }
        if (found) return value;
      });
      paginationData.pageIndex = listFilter.length > (paginationData.pageIndex * paginationData.pageSize) ? paginationData.pageIndex : 0;
    }

    paginationData.length = listFilter.length;
    const { _, pageIndex, pageSize, length } = paginationData;
    const [start, end] = [pageIndex * pageSize, length > ((pageIndex + 1) * pageSize) ? ((pageIndex + 1) * pageSize) : length]
    console.log(start, end);

    let result: T[] = [];
    for (let i = start; i < end; i++) {
      result.push(listFilter[i]);
    }

    return result;
  }
}

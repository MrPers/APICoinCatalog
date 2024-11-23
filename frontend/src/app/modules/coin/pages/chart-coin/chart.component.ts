import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Chart } from 'chart.js/auto';
import { dataCharts, timeStep } from '../../../../../services/constants';
import { ErrorService } from '../../../../../services/error';
import { FullChart, ICoinExchange, ITimeStep } from '../../../../../services/types';
import { URLService } from '../../../../../services/url';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartCoinComponent implements OnInit {

  private id: number;
  labelTime: string[] = [];
  contactMethods = dataCharts;
  constatadataChart: number= 0;
  fullChart: FullChart = new FullChart();
  myChart: any;
  loading = false;
  timesStep: ITimeStep[] = [];
  timeStep: number;
  

  constructor(private route:ActivatedRoute, private url:URLService, private errorService: ErrorService) {}

  ngOnInit()
  {
    this.id =this.route.snapshot.params['id'];
    this.timesStep = timeStep;
    this.timeStep = this.timesStep[0].time;
    this.loading = true;
    this.getCoinById();
    this.getCoinsById();
  }

  chooseStep(id: number) {
    this.timeStep = id;
    this.myChart.destroy();
    this.loading = true;
    this.getCoinsById();
  }

  onChange(id: number) {
    this.constatadataChart = id;
    this.myChart.destroy();
    this.drawChart(id);
  }

  drawChart(id: number) {
    this.myChart = new Chart("myChart", {
      type: 'line',
      data: {
          labels: this.labelTime,
          datasets: [{
            fill: {
              target: 'origin',
              above: this.contactMethods[id].above,
            },
            label: this.contactMethods[id].label,
            data: this.contactMethods[id].volume,
            backgroundColor: this.contactMethods[id].backgroundColor,
        }]
      },
      options: {
        plugins: {
          legend: {
            display: false
          }
        },
        scales: {
          y: {
            title: {
              display: true,
              text: 'Price',
              font: {                  
                family:'Helvetica Neue',
                size: 17
              }
            }
          },
          x: {
            title: {
              display: true,
              text: 'Period',
              font: {
                family:'Helvetica Neue',
                size: 17
              }
            }
          }
        }    
      }
   });
  }

  getCoinById() {
    this.url.getCoinById(this.id)
    .subscribe({
      next: (result: FullChart) => this.fullChart = result,
      error: (e:HttpErrorResponse) => this.errorService.handle(e.error)      
    });
  }

  getCoinsById() {
    this.url.getCoinsById({id: this.id, step: this.timeStep})
    .subscribe({
      next: (result: ICoinExchange[]) =>
      {
        this.contactMethods[0].volume =[];
        this.contactMethods[1].volume =[];
        this.labelTime =[];

        for (let index = 0; index < result.length; index++) {
          this.contactMethods[0].volume.push(result[index].prices);
          this.contactMethods[1].volume.push(result[index].volumeTraded);
          this.labelTime.push(new Date(result[index].time).toLocaleString());
        }
        this.loading = false;
        this.drawChart(this.constatadataChart);
      },
      error: (e:HttpErrorResponse) => this.errorService.handle(e.error)  
    });
  }
}
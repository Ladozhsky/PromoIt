
import { RetweetService } from '../services/retweet.services';
import { ErrorService } from '../services/error.service';
import { Request, Response, NextFunction } from 'express';
import { systemError, retweet, campaign } from '../entities';
import { ResponseHelper } from '../helpers/response.helper';
import { getEnvironmentData } from 'worker_threads';

const errorService: ErrorService = new ErrorService();
const retweetService: RetweetService = new RetweetService(errorService);


const newScope =    
    retweetService.getRetweets().then((res) => {
        console.log(res)
    })


// console.log(newScope())
 // const myVar = "asd"
    // const scope: retweet[] = []
    // retweetService.getRetweets().then((res) => scope = res)
    // // console.log(scope)
    // return myVar
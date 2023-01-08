import * as _ from "underscore";
import { Queries } from "../constants";
import { systemError, twitterUserId } from "../entities";
import { SqlHelper } from "../helpers/sql.helper";
import { ErrorService } from "./error.service";
import express from 'express';

interface localTwitterUserId {
    twitterUserId: string;
}


interface ITwitterUserIdService {
  getTwitterUserIds(): Promise<twitterUserId[]>;
}

export class TwitterUserIdService implements ITwitterUserIdService {
  private _errorService: ErrorService;

  constructor(private errorService: ErrorService) {
    this._errorService = errorService;
  }

  public getTwitterUserIds(): Promise<twitterUserId[]> {
    return new Promise<twitterUserId[]>((resolve, reject) => {
      const result: twitterUserId[] = [];

      SqlHelper.executeQueryArrayResult<localTwitterUserId>(
        this._errorService,
        Queries.TwitterUserIds
      )
        .then((queryResult: localTwitterUserId[]) => {
          queryResult.forEach((twitterUserId: localTwitterUserId) => {
            result.push(this.parselocalTwitterUserId(twitterUserId));
          });

          resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }

  private parselocalTwitterUserId(local: localTwitterUserId): twitterUserId {
    return {
      twitterUserId: local.twitterUserId,
    };
  }
}

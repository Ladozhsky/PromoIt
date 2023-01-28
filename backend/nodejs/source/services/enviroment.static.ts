import { environment } from "../entities";
import * as dotenv from "dotenv";
dotenv.config();

export class StaticEnvironment {
  public static environment: environment;

  public static dbConnectionString: string = process.env
    .DB_CONNECTION_STRING as string;
}

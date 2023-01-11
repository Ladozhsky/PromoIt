import addRetweets from "../controllers/twitter.controllet";
import { ListCreation } from "../services/list-creation.services";

const retweetList : ListCreation = new ListCreation

addRetweets(retweetList.createListOfRetweets())

import express from 'express';
import controller from '../controllers/retweet.controller';

const router = express.Router();

router.get('/retweets', controller.getRetweets);
router.get('/retweets-by-userId/:id', controller.getRetweetsByUserId);
router.post('/retweets', controller.addRetweet);



export default { router };
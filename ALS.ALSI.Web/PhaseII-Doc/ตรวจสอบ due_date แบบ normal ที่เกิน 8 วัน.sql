SELECT job_info.id,job_sample.id,job_sample.job_number,job_info.date_of_receive,job_sample.due_date,DATEDIFF(job_sample.due_date,job_info.date_of_receive) FROM job_sample
left join job_info on job_sample.job_id = job_info.id
where job_sample.status_completion_scheduled=1 and DATEDIFF(job_sample.due_date,job_info.date_of_receive) =9;




-- update job_sample set due_date = date_add(due_date, INTERVAL -1 DAY) where id=4207;


select * from job_sample where id=4207;

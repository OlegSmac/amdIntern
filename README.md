# amdIntern
Database structure:
<img width="1719" height="1080" alt="Vehicles_database" src="https://github.com/user-attachments/assets/be91be13-ad28-4d74-b647-c1ea6885edcc" />

Application pages:
![Main_page](https://github.com/user-attachments/assets/bc9d3158-8f27-49c3-a2b9-bd2dbd22130c)
![Post_page](https://github.com/user-attachments/assets/7c7a3f1d-bd19-4cf8-8c46-2f18abc2bcde)
![Posts_managing](https://github.com/user-attachments/assets/5f9fe0a8-ff3e-46ae-8a79-ab422b5811d7)
![Company_statistics](https://github.com/user-attachments/assets/938ef8a0-2de0-4f93-a748-fd4eb83874f7)

User types:
1. Regular user 
- Can see all vehicles from each company and add them to his favorite list.
- When a user adds a car to his favorite list, a notification is sent to a company account.
- Can send messages to the company's email. (smtp4dev)
- Can subscribe to a company to get notifications about new posts.


3. Company account
- When a company posts a new vehicle, users, who are subscribed to this company will get notification about the new post.
- Can see statistics: how many and what vehicle types were added to favorite lists (which vehicles are popular)
- Can answer regular users using email.


4. Admin
- Can manage all posts on the platform.
- Responsible for adding new vehicle models to the system.

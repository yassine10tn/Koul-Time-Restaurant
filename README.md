# 🍽️ Koul_time — Application de Commande de Restaurant

Une application web de gestion de restaurant développée avec **ASP.NET Core MVC** (.NET 8), permettant aux clients de consulter le menu, passer des commandes et suivre leur livraison, et aux administrateurs de gérer l'ensemble du système.

---

## 👥 Développeurs

- **Yassine Amri**
- **Adem Hamroun**

---

## 🚀 Fonctionnalités

### 👤 Côté Client
- Inscription / Connexion
- Consultation du menu par catégorie
- Recherche de plats
- Ajout au panier & modification des quantités
- Passage de commande (Livraison ou Pickup)
- Suivi de commande en temps réel
- Historique des commandes

### ⚙️ Côté Admin
- Dashboard avec statistiques et graphiques
- Gestion du menu (ajouter, modifier, supprimer des plats)
- Gestion des commandes (mise à jour des statuts)
- Gestion des utilisateurs (ajouter, modifier, supprimer, changer les rôles)

---

## 🛠️ Technologies utilisées

| Technologie | Usage |
|---|---|
| ASP.NET Core MVC (.NET 8) | Framework principal |
| C# | Langage backend |
| Razor Pages (.cshtml) | Vues frontend |
| Bootstrap 5 | Design & responsive |
| Bootstrap Icons | Icônes |
| Chart.js | Graphiques dashboard |
| Sessions ASP.NET | Authentification & panier |
| Mock Repository | Données en mémoire (sans base de données) |

---

## 🏗️ Architecture
RestaurantApp/
├── Controllers/
│   ├── AdminController.cs
│   ├── AuthController.cs
│   ├── CartController.cs
│   ├── HomeController.cs
│   ├── MenuController.cs
│   └── OrderController.cs
├── Data/
│   ├── Interfaces/
│   │   ├── IMenuRepository.cs
│   │   ├── IOrderRepository.cs
│   │   └── IUserRepository.cs
│   └── Mock/
│       ├── MockMenuRepository.cs
│       ├── MockOrderRepository.cs
│       └── MockUserRepository.cs
├── Models/
│   ├── Cart.cs
│   ├── CartItem.cs
│   ├── Category.cs
│   ├── MenuItem.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   └── User.cs
├── ViewModels/
│   ├── AdminViewModel.cs
│   ├── AuthViewModel.cs
│   ├── CheckoutViewModel.cs
│   ├── MenuViewModel.cs
│   └── OrderTrackingViewModel.cs
└── Views/
├── Admin/
│   ├── Dashboard.cshtml
│   ├── Menu.cshtml
│   ├── Orders.cshtml
│   └── Users.cshtml
├── Auth/
├── Cart/
├── Menu/
├── Order/
└── Shared/
└── _Layout.cshtml

---

## ⚙️ Installation & Lancement

### Prérequis
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou VS Code

### Étapes

1. **Cloner le dépôt**
```bash
git clone https://github.com/yassine10tn/Koul-Time-Restaurant.git
cd Koul-Time-Restaurant
```

2. **Ouvrir le projet**
```bash
# Visual Studio : ouvrir RestaurantApp.sln
# VS Code :
code .
```

3. **Lancer l'application**
```bash
dotnet run
```

4. **Accéder à l'application**
http://localhost:5000

---

## 🔐 Comptes de test

| Rôle | Email | Mot de passe |
|---|---|---|
| Admin | admin@restaurant.com | admin123 |
| Client | client@test.com | client123 |
| Client | yassine@test.com | pass123 |

---

## 💰 Devise

L'application utilise le **Dinar Tunisien (DT)** avec 3 décimales (millimes).

---

## 📸 Aperçu

### Menu
- Affichage par catégories : Entrées, Plats principaux, Pizzas, Desserts, Boissons
- Recherche en temps réel
- Prix en DT

### Panier
- Gestion des quantités
- Récapitulatif de commande

### Dashboard Admin
- KPIs : total commandes, en cours, livrées, chiffre d'affaires
- Graphique des plats les plus vendus (Doughnut)
- Graphique des revenus par jour (Line)
- Répartition des statuts (Bar)
- Tableau des dernières commandes
- Gestion complète des utilisateurs

---

## 📝 Notes

> Ce projet utilise un système de **Mock Repository** (données en mémoire).
> Les données sont réinitialisées à chaque redémarrage de l'application.
> Pour une version production, remplacer les Mock par une vraie base de données (SQL Server, PostgreSQL, etc.)
